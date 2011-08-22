using System;

namespace Petroledger.Data.Model
{
    public partial class FillupEntry
    {
        

        public DateTime EntryDate { get; set; }
        public double FillAmount { get; set; }
        public double PricePerUnit { get; set; }
        public double OdometerReading { get; set; }
        public bool WasNotToppedOff { get; set; }
        public bool PreviousEntryMissed { get; set; }
        public UnitOfMeasure.Volume PumpUnit { get; set; }
        public UnitOfMeasure.Distance OdometerUnit { get; set; }
    }
}