using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using Petroledger.Data.Model;

namespace Petroledger.ViewModels
{
    public class MainViewModel
    {
        private readonly ObservableCollection<VehicleSummaryViewModel> _vehiclePages;
        private Vehicle _selectedVehicle;

        public MainViewModel()
        {
            _vehiclePages = new ObservableCollection<VehicleSummaryViewModel>();
            App.AnalysisCacheInvalidated += OnAnalysisCacheInvalidated;
        }

        public Vehicle SelectedVehicle
        {
            get { return _selectedVehicle; }
            set
            {
                Debug.WriteLine("SelectedVehicle set to {0}", value != null ? value.VehicleName : "null");
                _selectedVehicle = value;
            }
        }

        public ObservableCollection<VehicleSummaryViewModel> VehiclePages
        {
            get { return _vehiclePages; }
        }

        public void OnAnalysisCacheInvalidated(Vehicle vehicle)
        {
            var vehiclePage = _vehiclePages.Where(page => page.Vehicle.Equals(vehicle))
                .SingleOrDefault();

            /* 
             * since there is a static reference to this class in App.xaml.cs,
             * this method can fire even if the MainPage was never shown
             */
            if(vehiclePage != null) vehiclePage.IsSummaryCurrent = false;

        }
        
    }
}