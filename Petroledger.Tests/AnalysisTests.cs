using System;
using Microsoft.Silverlight.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Petroledger.Data;
using Petroledger.Data.Model;
using Petroledger.Exceptions;
using Petroledger.Extensions;

namespace Petroledger.Tests
{
    [TestClass]
    public class AnalysisTests : SilverlightTest
    {
        [TestMethod]
        [ExpectedException(typeof (NotEnoughSampleDataException))]
        public void TestNotEnoughSampleDataTwoEntriesPreviousEntryMissed()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};

            var entry1 = new FillupEntry
                             {
                                 OdometerReading = 100,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 EntryDate = new DateTime(2010,10,1)
                             };

            var entry2 = new FillupEntry
                             {
                                 OdometerReading = 200,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 PreviousEntryMissed = true,
                                 EntryDate = new DateTime(2010,10,08)
                             };

            vehicle.Entries.Add(entry1);
            vehicle.Entries.Add(entry2);
            vehicle.CalculateEfficiency(entry2, entry1, UnitOfMeasure.DefaultDistanceUnit,
                                        UnitOfMeasure.DefaultVolumeUnit);
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException),
            "The baseline odometer reading cannot be greater than or equal to the entry's odometer reading.")]
        public void TestForArgumentExceptionBaselineOdometerReadingGreater()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};

            var entry1 = new FillupEntry
                             {
                                 OdometerReading = 100,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 EntryDate = new DateTime(2010,10,1)
                             };

            var entry2 = new FillupEntry
                             {
                                 OdometerReading = 200,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 EntryDate = new DateTime(2010,10,08)
                             };

            vehicle.Entries.Add(entry1);
            vehicle.Entries.Add(entry2);

            vehicle.CalculateEfficiency(entry1, entry2, UnitOfMeasure.DefaultDistanceUnit,
                                        UnitOfMeasure.DefaultVolumeUnit);
        }

        [TestMethod]
        [ExpectedException(typeof (NotEnoughSampleDataException))]
        public void TestNotEnoughSampleDataTwoEntriesWasNotToppedOff()
        {
            var vehicle = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};

            var entry1 = new FillupEntry
                             {
                                 OdometerReading = 100,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 EntryDate = new DateTime(2010,10,1)
                             };

            var entry2 = new FillupEntry
                             {
                                 OdometerReading = 200,
                                 FillAmount = 14.436,
                                 PricePerUnit = 2.799,
                                 PumpUnit = UnitOfMeasure.Volume.Liter,
                                 EntryDate = new DateTime(2010,10,08),
                                 WasNotToppedOff = true
                             };

            vehicle.Entries.Add(entry1);
            vehicle.Entries.Add(entry2);

            vehicle.CalculateEfficiency(entry2, entry1, UnitOfMeasure.DefaultDistanceUnit,
                                        UnitOfMeasure.DefaultVolumeUnit);
        }

        [TestMethod]
        public void TestNormalizeToMetric()
        {
            var entry = new FillupEntry
                            {
                                FillAmount = 1,
                                OdometerReading = 1,
                                PumpUnit = UnitOfMeasure.Volume.Gallon,
                                OdometerUnit = UnitOfMeasure.Distance.Mile,
                                PricePerUnit = 1
                            };
            FillupEntry entry2 = FillupEntry.Clone(entry);

            entry2.Normalize(UnitOfMeasure.Distance.Kilometer, UnitOfMeasure.Volume.Liter);

            Assert.IsTrue(entry.PumpUnit == UnitOfMeasure.Volume.Gallon
                          && entry2.PumpUnit == UnitOfMeasure.Volume.Liter);

            Assert.IsTrue(entry.OdometerUnit == UnitOfMeasure.Distance.Mile
                          && entry2.OdometerUnit == UnitOfMeasure.Distance.Kilometer);

            Assert.IsTrue(entry2.FillAmount == entry.FillAmount*3.78541178);

            Assert.IsNotNull(entry2.OdometerReading == entry.OdometerReading*1.609344);
        }

        [TestMethod]
        public void TestNormalizeToImperial()
        {
            var entry = new FillupEntry
                            {
                                FillAmount = 1,
                                OdometerReading = 1,
                                PumpUnit = UnitOfMeasure.Volume.Liter,
                                OdometerUnit = UnitOfMeasure.Distance.Kilometer,
                                PricePerUnit = 1
                            };
            FillupEntry entry2 = FillupEntry.Clone(entry);

            entry2.Normalize(UnitOfMeasure.Distance.Mile, UnitOfMeasure.Volume.Gallon);

            Assert.IsTrue(entry.PumpUnit == UnitOfMeasure.Volume.Liter
                          && entry2.PumpUnit == UnitOfMeasure.Volume.Gallon);

            Assert.IsTrue(entry.OdometerUnit == UnitOfMeasure.Distance.Kilometer
                          && entry2.OdometerUnit == UnitOfMeasure.Distance.Mile);

            Assert.IsTrue(entry2.FillAmount == entry.FillAmount*0.264172052);
            Assert.IsNotNull(entry2.OdometerReading == entry.OdometerReading*0.621371192);
        }
    }
}