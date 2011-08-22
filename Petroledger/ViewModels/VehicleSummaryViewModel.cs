using System.ComponentModel;
using System.Text;
using Petroledger.Data.Analysis;
using Petroledger.Data.Model;

namespace Petroledger.ViewModels
{
    public class VehicleSummaryViewModel : INotifyPropertyChanged
    {
        private Vehicle _vehicle;
        private VehicleAnalysis _vehicleAnalysis;
        public bool IsSummaryCurrent { get; set; }

        public string NotEnoughSampleDataLabel
        {
            get
            {
                if (_vehicleAnalysis.FillupEntryAnalyses.Count > 0)
                    return null;

                if (_vehicle.Entries.Count == 0)
                    return "you must record two more fill-ups before statistics can be generated.";

                if (_vehicle.Entries.Count == 1)
                    return "you must record one more fill-up before statistics can be generated.";

                //we got here because there are not enough entrys where the tank was topped off
                //and the previous entry wasn't missed.
                return
                    "you must record two consecutive fill-ups in which the tank was topped off before statistics can be calculated";
            }
        }

        public string AverageCostEfficiencyLabel
        {
            get
            {
                return _vehicleAnalysis.CostEfficiency != null
                           ? string.Format("{0:C3} per {1}", _vehicleAnalysis.CostEfficiency,
                                           _vehicleAnalysis.DistanceUnit.ToString().ToLower())
                           : null;
            }
        }

        public string AverageFuelEfficiencyLabel
        {
            get
            {
                return _vehicleAnalysis.FuelEfficiency != null
                           ? _vehicleAnalysis.FuelEfficiency.Value.GetEfficiencyString(_vehicleAnalysis.DistanceUnit,
                                                                                       _vehicleAnalysis.VolumeUnit)
                           : null;
            }
        }

        public string AverageTimeBetweenFillups
        {
            get
            {
                return _vehicleAnalysis.AverageTimeBetweenFillups != null
                           ? _vehicleAnalysis.AverageTimeBetweenFillups.Value.Days + " days between fill-ups (avg)"
                           : null;
            }
        }

        public string AverageDistanceDrivenBetweenFillups
        {
            get
            {
                return _vehicleAnalysis.AverageDistanceDrivenBetweenFillups != null
                           ? string.Format("{0:####.#} {1}s driven between fill-ups (avg)",
                                           _vehicleAnalysis.AverageDistanceDrivenBetweenFillups,
                                           _vehicleAnalysis.DistanceUnit)
                           : null;
            }
        }

        public string AnalysisDatesLabel
        {
            get
            {
                var sb = new StringBuilder();
                if (_vehicleAnalysis.FillupEntryAnalyses.Count > 0)
                    sb.AppendFormat("analyzed {0} entries from {1:d} to {2:d} covering {3:###,###.#} {4}s.  ",
                                    _vehicleAnalysis.FillupEntryAnalyses.Count + 1, _vehicleAnalysis.FromDate,
                                    _vehicleAnalysis.ToDate, _vehicleAnalysis.TotalDistanceDriven,
                                    _vehicleAnalysis.DistanceUnit.ToString().ToLower());

                if (_vehicleAnalysis.SkippedEntriesDueToPreviousMissed > 0)
                {
                    string fillupVersion = _vehicleAnalysis.SkippedEntriesDueToPreviousMissed == 1
                                               ? "fill-up"
                                               : "fill-ups";
                    sb.AppendFormat(
                        "{0} {1} could not be analyzed because the previous entry was missed.  ",
                        _vehicleAnalysis.SkippedEntriesDueToPreviousMissed, fillupVersion);
                }

                if (_vehicleAnalysis.SkippedEntriesDueToTopOff > 0)
                {
                    string fillupVersion = _vehicleAnalysis.SkippedEntriesDueToTopOff == 1
                                               ? "fill-up"
                                               : "fill-ups";

                    sb.AppendFormat("{0} {1} could not be analyzed because the tank was not topped off.",
                                    _vehicleAnalysis.SkippedEntriesDueToTopOff, fillupVersion);
                }
                return sb.Length > 0
                           ? sb.ToString()
                           : null;
            }
        }

        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Vehicle"));
                UpdateSummary();
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        public void UpdateSummary()
        {
            System.Diagnostics.Debug.WriteLine("VehicleSummaryViewMode.UpdateSummary({0}) called.", Vehicle.VehicleName);
            _vehicleAnalysis = App.GetVehicleAnalysis(_vehicle);

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("NotEnoughSampleDataLabel"));
                PropertyChanged(this, new PropertyChangedEventArgs("AverageFuelEfficiencyLabel"));
                PropertyChanged(this, new PropertyChangedEventArgs("AverageCostEfficiencyLabel"));
                PropertyChanged(this, new PropertyChangedEventArgs("TotalDistanceDrivenLabel"));
                PropertyChanged(this, new PropertyChangedEventArgs("AnalysisDatesLabel"));
            }

            IsSummaryCurrent = true;
        }
    }
}