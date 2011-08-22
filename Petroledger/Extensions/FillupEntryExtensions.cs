using System;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Petroledger.Data;
using Petroledger.Data.Model;
using Petroledger.Exceptions;

namespace Petroledger.Extensions
{
    public static class FillupEntryExtensions
    {
        private const double KILOMETER_TO_MILE_RATIO = 0.621371192;
        private const double MILE_TO_KILOMETER_RATIO = 1.609344;
        private const double LITER_TO_GALLON_RATIO = 0.264172052;
        private const double GALLON_TO_LITER_RATIO = 3.78541178;

        public static void Normalize(this FillupEntry entry,
                                     UnitOfMeasure.Distance normalizedDistanceUnit,
                                     UnitOfMeasure.Volume normalizedVolumeUnit)
        {
            if (entry.OdometerUnit != normalizedDistanceUnit)
            {
                entry.OdometerReading *= entry.OdometerUnit == UnitOfMeasure.Distance.Mile
                                             ? MILE_TO_KILOMETER_RATIO
                                             : KILOMETER_TO_MILE_RATIO;

                entry.OdometerUnit = entry.OdometerUnit == UnitOfMeasure.Distance.Mile
                                         ? UnitOfMeasure.Distance.Kilometer
                                         : UnitOfMeasure.Distance.Mile;
            }

            if (entry.PumpUnit != normalizedVolumeUnit)
            {
                entry.FillAmount *= entry.PumpUnit == UnitOfMeasure.Volume.Gallon
                                        ? GALLON_TO_LITER_RATIO
                                        : LITER_TO_GALLON_RATIO;

                entry.PricePerUnit /= entry.PumpUnit == UnitOfMeasure.Volume.Gallon
                                          ? GALLON_TO_LITER_RATIO
                                          : LITER_TO_GALLON_RATIO;

                entry.PumpUnit = entry.PumpUnit == UnitOfMeasure.Volume.Gallon
                                     ? UnitOfMeasure.Volume.Liter
                                     : UnitOfMeasure.Volume.Gallon;
            }
        }


        public static byte[] ToBytes(this FillupEntry fillupEntry)
        {
            var bytes = new byte[FillupEntry.SIZE_IN_BYTES];

            int curOffset = 0;

            Array.Copy(BitConverter.GetBytes(fillupEntry.EntryDate.Ticks), 0, bytes, curOffset, sizeof (long));
            curOffset += sizeof (long);

            Array.Copy(BitConverter.GetBytes(fillupEntry.OdometerReading), 0, bytes, curOffset, sizeof (double));
            curOffset += sizeof (double);

            Array.Copy(BitConverter.GetBytes(fillupEntry.FillAmount), 0, bytes, curOffset, sizeof (double));
            curOffset += sizeof (double);

            Array.Copy(BitConverter.GetBytes(fillupEntry.PricePerUnit), 0, bytes, curOffset, sizeof (double));
            curOffset += sizeof (double);

            Array.Copy(new[] {(byte) fillupEntry.PumpUnit}, 0, bytes, curOffset, sizeof (byte));
            curOffset += sizeof (byte);

            Array.Copy(new[] {(byte) fillupEntry.OdometerUnit}, 0, bytes, curOffset, sizeof (byte));
            curOffset += sizeof (byte);

            Array.Copy(BitConverter.GetBytes(fillupEntry.WasNotToppedOff), 0, bytes, curOffset, sizeof (bool));
            curOffset += sizeof (bool);

            Array.Copy(BitConverter.GetBytes(fillupEntry.PreviousEntryMissed), 0, bytes, curOffset, sizeof (bool));
            curOffset += sizeof (bool);

            return bytes;
        }

        public static FillupEntry EntryFromBytes(byte[] bytes)
        {
            var entry = new FillupEntry();
            int curOffset = 0;

            var fillAmountBytes = new byte[sizeof (double)];
            var fillupDateTimeBytes = new byte[sizeof (long)];
            var odometerReadingBytes = new byte[sizeof (double)];
            var pricePerVolumeUnitBytes = new byte[sizeof (double)];
            var volumeUnitBytes = new byte[sizeof (byte)];
            var odometerDistanceUnitBytes = new byte[sizeof (byte)];
            var wasNotToppedOffBytes = new byte[sizeof (bool)];
            var previousEntryMissedBytes = new byte[sizeof (bool)];

            Array.Copy(bytes, curOffset, fillupDateTimeBytes, 0, fillupDateTimeBytes.Length);
            entry.EntryDate = new DateTime(BitConverter.ToInt64(fillupDateTimeBytes, 0));
            curOffset += sizeof (long);

            Array.Copy(bytes, curOffset, odometerReadingBytes, 0, odometerReadingBytes.Length);
            entry.OdometerReading = BitConverter.ToDouble(odometerReadingBytes, 0);
            curOffset += sizeof (double);

            Array.Copy(bytes, curOffset, fillAmountBytes, 0, fillAmountBytes.Length);
            entry.FillAmount = BitConverter.ToDouble(fillAmountBytes, 0);
            curOffset += sizeof (double);

            Array.Copy(bytes, curOffset, pricePerVolumeUnitBytes, 0, pricePerVolumeUnitBytes.Length);
            entry.PricePerUnit = BitConverter.ToDouble(pricePerVolumeUnitBytes, 0);
            curOffset += sizeof (double);

            Array.Copy(bytes, curOffset, volumeUnitBytes, 0, volumeUnitBytes.Length);
            entry.PumpUnit = (UnitOfMeasure.Volume) volumeUnitBytes[0];
            curOffset += sizeof (byte);

            Array.Copy(bytes, curOffset, odometerDistanceUnitBytes, 0, odometerDistanceUnitBytes.Length);
            entry.OdometerUnit = (UnitOfMeasure.Distance) odometerDistanceUnitBytes[0];
            curOffset += sizeof (byte);

            Array.Copy(bytes, curOffset, wasNotToppedOffBytes, 0, wasNotToppedOffBytes.Length);
            entry.WasNotToppedOff = BitConverter.ToBoolean(wasNotToppedOffBytes, 0);
            curOffset += sizeof (bool);

            Array.Copy(bytes, curOffset, previousEntryMissedBytes, 0, previousEntryMissedBytes.Length);
            entry.PreviousEntryMissed = BitConverter.ToBoolean(previousEntryMissedBytes, 0);
            curOffset += sizeof (bool);

            return entry;
        }

        public static void Validate(this FillupEntry entry, Vehicle vehicle)
        {
            Debug.Assert(entry.OdometerUnit == vehicle.OdometerUnit,
                         "entry odometer unit must equal vehicle odometer unit.");
            Debug.Assert(entry.EntryDate != default(DateTime));

            if (entry.EntryDate > DateTime.Now)
                throw new PetroledgerValidationException("The entry date cannot be in the future.");

            if (entry.OdometerReading == -1)
                throw new PetroledgerValidationException("The odometer reading entered is not valid.");

            if (entry.OdometerReading <= 0)
                throw new PetroledgerValidationException("The odometer reading must be greater than 0.");

            if (entry.FillAmount == -1)
                throw new PetroledgerValidationException("The fill amount entered is not valid.");

            if (entry.FillAmount <= 0)
                throw new PetroledgerValidationException("The fill amount must be greater than 0.");

            if (entry.PricePerUnit == -1)
                throw new PetroledgerValidationException("The price entered is not valid.");

            if (entry.PricePerUnit <= 0)
                throw new PetroledgerValidationException("The price must be greater than 0.");

            if (entry.EntryDate > DateTime.Now)
            {
                throw new PetroledgerValidationException("Entry date cannot be in the future.");
            }

            FillupEntry previousEntry =
                vehicle.Entries
                    .Where(e => e.EntryDate < entry.EntryDate)
                    .OrderByDescending(e => e.EntryDate)
                    .FirstOrDefault();

            FillupEntry subsequentEntry =
                vehicle.Entries.Where(e => e.EntryDate > entry.EntryDate)
                    .OrderBy(e => e.EntryDate)
                    .FirstOrDefault();

            if (previousEntry != null && previousEntry.OdometerReading > entry.OdometerReading)
                throw new PetroledgerValidationException(
                    String.Format(
                        "An entry from {0:d} has an odometer reading of {1}.  The odometer reading must be greater than {1}.",
                        previousEntry.EntryDate, previousEntry.OdometerReading));

            if (subsequentEntry != null && subsequentEntry.OdometerReading < entry.OdometerReading)
                throw new PetroledgerValidationException(
                    String.Format(
                        "An entry from {0:d} has an odometer reading of {1}.  The odometer reading must be less than {1}.",
                        subsequentEntry.EntryDate, subsequentEntry.OdometerReading));
        }
    }
}