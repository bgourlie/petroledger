using System;
using Petroledger.Data.Model;

namespace Petroledger.Data.Analysis
{
    public class FillupEntryAnalysis
    {
        public FillupEntry BaselineEntry { get; set; }
        public FillupEntry CalculatedEntry { get; set; }
        public UnitOfMeasure.Distance DistanceUnit { get; set; }
        public UnitOfMeasure.Volume VolumeUnit { get; set; }
        public double FuelEfficiency { get; set; }
        public double? CostEfficiency { get; set; }
        public double DistanceDriven { get; set; }
        public TimeSpan TimeBetweenFillups { get; set; }
    }
}