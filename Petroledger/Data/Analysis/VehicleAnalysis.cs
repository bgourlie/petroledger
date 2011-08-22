using System;
using System.Collections.Generic;
using Petroledger.Data.Model;

namespace Petroledger.Data.Analysis
{
    public class VehicleAnalysis
    {
        public UnitOfMeasure.Distance DistanceUnit;
        public UnitOfMeasure.Volume VolumeUnit;
        public List<FillupEntryAnalysis> FillupEntryAnalyses { get; set; }
        public Vehicle Vehicle { get; set; }
        public double? FuelEfficiency { get; set; }
        public double? CostEfficiency { get; set; }
        public string CostEfficiencyCurrencySymbol { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public double? TotalDistanceDriven { get; set; }
        public TimeSpan? AverageTimeBetweenFillups { get; set; }
        public double? AverageDistanceDrivenBetweenFillups { get; set; }
        public double? TotalPaidAtPump { get; set; }
        public double? AveragePaidAtPump { get; set; }

        public int SkippedEntriesDueToTopOff { get; set; }
        public int SkippedEntriesDueToPreviousMissed { get; set; }
    }
}