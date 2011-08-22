using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using Petroledger.Data;
using Petroledger.Data.Analysis;
using Petroledger.Data.Model;
using Petroledger.Exceptions;

namespace Petroledger.Extensions
{
    public static class VehicleExtensions
    {
        public static VehicleAnalysis GenerateVehicleAnalysis(this Vehicle vehicle, UnitOfMeasure.Distance distanceUnit,
                                                              UnitOfMeasure.Volume volumeUnit)
        {
            Debug.WriteLine("Generating Analysis for {0}", vehicle.VehicleName);

            var vehicleAnalysis = new VehicleAnalysis
                                      {
                                          Vehicle = vehicle,
                                          DistanceUnit = distanceUnit,
                                          VolumeUnit = volumeUnit,
                                          FillupEntryAnalyses = new List<FillupEntryAnalysis>(),
                                          SkippedEntriesDueToPreviousMissed = 0,
                                          SkippedEntriesDueToTopOff = 0
                                      };

            if (vehicle.Entries.Count > 1)
            {
                for (int i = vehicle.Entries.Count - 1; i > 0; i--)
                {
                    var calculatedEntry = vehicle.Entries[i];
                    var baselineEntry = vehicle.Entries[i - 1];

                    if (calculatedEntry.PreviousEntryMissed)
                    {
                        vehicleAnalysis.SkippedEntriesDueToPreviousMissed++;
                        continue;
                    }

                    if (calculatedEntry.WasNotToppedOff ||
                        baselineEntry.WasNotToppedOff)
                    {
                        vehicleAnalysis.SkippedEntriesDueToTopOff++;
                        continue;
                    }

                    vehicleAnalysis.FillupEntryAnalyses.Add(vehicle.CalculateEfficiency(calculatedEntry, baselineEntry,
                                                                                        distanceUnit, volumeUnit));
                }

                if (vehicleAnalysis.FillupEntryAnalyses.Count > 0)
                {
                    vehicleAnalysis.FromDate = vehicleAnalysis.FillupEntryAnalyses.Min(a => a.BaselineEntry.EntryDate);
                    vehicleAnalysis.ToDate = vehicleAnalysis.FillupEntryAnalyses.Max(a => a.CalculatedEntry.EntryDate);

                    vehicleAnalysis.TotalDistanceDriven = vehicle.Entries.Max(e => e.OdometerReading) -
                                                          vehicle.Entries.Min(e => e.OdometerReading);

                    if (vehicleAnalysis.FillupEntryAnalyses.Count > 0)
                    {
                        //vehicleAnalysis.AverageTimeBetweenFillups = TimeSpan.FromTicks(
                        //    vehicleAnalysis.FillupEntryAnalyses.Sum(
                        //        e => e.CalculatedEntry.EntryDate.Ticks - e.BaselineEntry.EntryDate.Ticks)/
                        //    vehicleAnalysis.FillupEntryAnalyses.Count());

                        //vehicleAnalysis.AverageDistanceDrivenBetweenFillups = 
                        //    vehicleAnalysis.FillupEntryAnalyses.Sum(
                        //        e => e.CalculatedEntry.OdometerReading - e.BaselineEntry.OdometerReading) /
                        //    vehicleAnalysis.FillupEntryAnalyses.Count();

                        vehicleAnalysis.FuelEfficiency = vehicleAnalysis.FillupEntryAnalyses.Sum(e => e.FuelEfficiency)/
                                                         vehicleAnalysis.FillupEntryAnalyses.Count();

                        vehicleAnalysis.CostEfficiency =
                            vehicleAnalysis.FillupEntryAnalyses.Sum(e => e.CostEfficiency)/
                            vehicleAnalysis.FillupEntryAnalyses.Count();
                    }
                }
            }
            return vehicleAnalysis;
        }

        public static void SaveEntry(this Vehicle vehicle, FillupEntry fillupEntry)
        {
            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Debug.Assert(store.DirectoryExists(vehicle.Id.ToString()));
                string fileName = vehicle.Id + "\\" + fillupEntry.EntryDate.Ticks;

                using (var fileWriter = store.CreateFile(fileName))
                {
                    byte[] bytes = fillupEntry.ToBytes();
                    fileWriter.Write(bytes, 0, bytes.Length);
                }
            }
        }

        public static void Validate(this Vehicle vehicle)
        {
            if (String.IsNullOrEmpty(vehicle.VehicleName) || vehicle.VehicleName.Trim().Length < 3)
                throw new PetroledgerValidationException("The vehicle name must be atleast 3 characters.");

            if (vehicle.Year == -1)
                throw new PetroledgerValidationException("The year is not a valid number.");

            if (vehicle.Year != 0 && vehicle.Year + 1 > DateTime.Now.Year)
                throw new PetroledgerValidationException("The year cannot be in the future.");
        }

        public static void LoadEntriesFromStorage(this Vehicle vehicle)
        {
            if (vehicle.Entries.Count > 0)
                throw new InvalidOperationException(
                    "Cannot call LoadEntriesFromStorage if entries have already been initialized.");

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                foreach (string entryName in
                    store.GetFileNames(vehicle.Id + " \\*").Where(entryName => !entryName.Equals("vehicle")))
                {
                    using (var entryFileStream = store.OpenFile(vehicle.Id + "\\" + entryName,
                                                                FileMode.Open,
                                                                FileAccess.Read))
                    {
                        var entryBytes = new byte[FillupEntry.SIZE_IN_BYTES];
                        entryFileStream.Read(entryBytes, 0, entryBytes.Length);
                        var entry = FillupEntryExtensions.EntryFromBytes(entryBytes);
                        vehicle.Entries.Add(entry);
                    }
                }

                vehicle.Entries.Sort(new EntryComparer());
            }
        }

        public static void UpdateExistingEntry(this Vehicle vehicle, FillupEntry entry, long originalEntryDateTicks)
        {
            if (!vehicle.Entries.Contains(entry))
                throw new ArgumentException("The entry must exist in the Vehicle's Entry collection.");

            if (entry.EntryDate.Ticks != originalEntryDateTicks)
            {
                using (var store = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    store.DeleteFile(vehicle.Id + "\\" + originalEntryDateTicks);
                }
            }

            vehicle.SaveEntry(entry);
        }

        public static void InsertEntry(this Vehicle vehicle, FillupEntry fillupEntry)
        {
            for (int i = vehicle.Entries.Count - 1; i >= 0; i--)
            {
                if (fillupEntry.EntryDate <= vehicle.Entries[i].EntryDate) continue;
                var previousEntry = vehicle.Entries[i];
                var subsequentEntry = i + 1 < vehicle.Entries.Count ? vehicle.Entries[i + 1] : null;

                if (previousEntry.OdometerReading >= fillupEntry.OdometerReading)
                    throw new ArgumentException(
                        "The fillup entry's odometer reading is less than or equal to a reading with an earlier date.");

                if (subsequentEntry != null && subsequentEntry.OdometerReading <= fillupEntry.OdometerReading)
                    throw new ArgumentException(
                        "The fillup entry's odometer reading is greater than or equal to a reading with a later date.");

                vehicle.Entries.Insert(i + 1, fillupEntry);
                return;
            }

            var subsequentEntry2 = vehicle.Entries.Count > 0 ? vehicle.Entries[0] : null;
            if (subsequentEntry2 != null && subsequentEntry2.OdometerReading <= fillupEntry.OdometerReading)
                throw new ArgumentException(
                    "The fillup entry's odometer reading is greater than or equal to a reading with a later date.");
            vehicle.Entries.Insert(0, fillupEntry);
        }

        public static FillupEntryAnalysis CalculateEfficiency(this Vehicle vehicle, FillupEntry calculatedEntry,
                                                              FillupEntry baselineEntry,
                                                              UnitOfMeasure.Distance normalizedDistanceUnit,
                                                              UnitOfMeasure.Volume normalizedVolumeUnit)
        {
            if (baselineEntry == null)
                throw new ArgumentException("Baseline entry cannot be null.");

            if (baselineEntry.OdometerReading > calculatedEntry.OdometerReading)
                throw new ArgumentException(
                    "The baseline odometer reading cannot be greater than or equal to the entry's odometer reading.");

            if (calculatedEntry.PreviousEntryMissed || calculatedEntry.WasNotToppedOff)
                throw new NotEnoughSampleDataException("Previous entry was missed or the tank was not topped off.");

            if (baselineEntry.WasNotToppedOff)
                throw new NotEnoughSampleDataException("The tank was not topped off for the baseline entry.");

            var normalizedEntry = FillupEntry.Clone(calculatedEntry);
            var normalizedBaselineEntry = FillupEntry.Clone(baselineEntry);

            normalizedEntry.Normalize(normalizedDistanceUnit, normalizedVolumeUnit);
            normalizedBaselineEntry.Normalize(normalizedDistanceUnit, normalizedVolumeUnit);

            double distanceDriven = normalizedEntry.OdometerReading - normalizedBaselineEntry.OdometerReading;

            return new FillupEntryAnalysis
                       {
                           DistanceUnit = normalizedDistanceUnit,
                           VolumeUnit = normalizedVolumeUnit,
                           FuelEfficiency = distanceDriven/normalizedEntry.FillAmount,
                           CostEfficiency = (normalizedEntry.FillAmount*normalizedEntry.PricePerUnit)/distanceDriven,
                           BaselineEntry = baselineEntry,
                           CalculatedEntry = calculatedEntry
                       };
        }

        public static void DeleteFillupEntry(this Vehicle vehicle, FillupEntry entry)
        {
            if (!vehicle.Entries.Contains(entry))
                throw new ArgumentException("The entry must exist in the Vehicle's Entry collection.");

            using (var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                var entryFileName = vehicle.Id + "\\" + entry.EntryDate.Ticks;
                store.DeleteFile(entryFileName);
            }
        }

        #region Nested type: EntryComparer

        private class EntryComparer : IComparer<FillupEntry>
        {
            #region IComparer<FillupEntry> Members

            public int Compare(FillupEntry x, FillupEntry y)
            {
                if (ReferenceEquals(x, y)) return 0;

                if (x.EntryDate == y.EntryDate)
                    throw new CorruptEntryException("Two entries with the same EntryDate");

                return x.EntryDate > y.EntryDate ? 1 : -1;
            }

            #endregion
        }

        #endregion
    }
}