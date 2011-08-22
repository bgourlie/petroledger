using System;

namespace Tools
{
    public class FillupEntry
    {
        public const int SIZE_IN_BYTES = 36;

        public DateTime EntryDate { get; set; }
        public double FillAmount { get; set; }
        public double PricePerUnit { get; set; }
        public double OdometerReading { get; set; }
        public bool WasNotToppedOff { get; set; }
        public bool PreviousEntryMissed { get; set; }       
        public UnitOfMeasure.Volume PumpUnit { get; set; }
        public UnitOfMeasure.Distance OdometerUnit { get; set; }

        public static FillupEntry Clone(FillupEntry entry)
        {
            return (FillupEntry)entry.MemberwiseClone();
        }
    }

}