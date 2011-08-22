using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Petroledger.Data;
using Petroledger.Data.Model;
using Petroledger.Exceptions;
using Petroledger.Extensions;
using Petroledger.ViewModels;

namespace Petroledger
{
    public partial class AddOrEditVehicle : PhoneApplicationPage
    {
        private Mode _mode;
        private bool _isNewInstance;
        private Vehicle _originalVehicle;

        public enum Mode
        {
            New,
            Edit,
        }

        public AddOrEditVehicle()
        {
            InitializeComponent();
            _isNewInstance = true;
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            StateUtils.PreserveState(State, txtNameBasic);
            StateUtils.PreserveState(State, txtMake);
            StateUtils.PreserveState(State, txtNameAdvanced);
            StateUtils.PreserveState(State, txtYear);
            StateUtils.PreserveState(State, optMiles);
            StateUtils.PreserveState(State, optKilometers);
            State["CurPivotItem"] = AddVehiclePivot.SelectedIndex == 0 ? "basic" : "advanced";
            State["StatePreserved"] = true;
            _isNewInstance = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (!_isNewInstance) return;

            string mode;
            NavigationContext.QueryString.TryGetValue("mode", out mode);
            _mode = (Mode)(Enum.Parse(typeof(Mode), mode, true));
            Vehicle viewModel;

            switch (_mode)
            {
                case Mode.New:
                    viewModel = new Vehicle {OdometerUnit = UnitOfMeasure.DefaultDistanceUnit};
                    break;

                case Mode.Edit:
                    string vehicleIdString;
                    NavigationContext.QueryString.TryGetValue("vehicleId", out vehicleIdString);
                    int vehicleId = int.Parse(vehicleIdString);
                    _originalVehicle = App.VehicleStore.Values
                        .Where(v => v.Id == vehicleId)
                        .Single();

                        //a quick and dirty way of cloning the vehicle
                        viewModel = Vehicle.FromBytes(Vehicle.ToBytes(_originalVehicle));
                    break;

                default:
                    throw new Exception("We should never get here.");
            }

            DataContext = viewModel;

            if (!State.ContainsKey("StatePreserved")) return;
            StateUtils.RestoreState(State, txtNameBasic, String.Empty);
            StateUtils.RestoreState(State, txtMake, String.Empty);
            StateUtils.RestoreState(State, txtNameAdvanced, String.Empty);
            StateUtils.RestoreState(State, txtYear, String.Empty);
            StateUtils.RestoreState(State, optMiles, UnitOfMeasure.DefaultDistanceUnit == UnitOfMeasure.Distance.Mile);
            StateUtils.RestoreState(State, optKilometers, UnitOfMeasure.DefaultDistanceUnit == UnitOfMeasure.Distance.Kilometer);
            AddVehiclePivot.SelectedIndex = State["CurPivotItem"] as string == "basic" ? 0 : 1;
        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = (Vehicle)DataContext;

            // ReSharper disable PossibleNullReferenceException
            if (AddVehiclePivot.SelectedIndex == 0)
            {
                //only updating binding source for name if we're in basic mode
                txtNameBasic.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                vehicle.OdometerUnit = UnitOfMeasure.DefaultDistanceUnit;
            }
            else
            {
                //otherwise update from the advanced fields
                txtMake.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtModel.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtYear.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                txtNameAdvanced.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            }
            // ReSharper restore PossibleNullReferenceException
            
            try
            {
                vehicle.Validate();

                switch(_mode)
                {
                    case Mode.New:
                        Vehicle.Save(vehicle);
                        App.VehicleStore.Add(vehicle.Id, vehicle);
                        App.MainViewModel.VehiclePages.Add(new VehicleSummaryViewModel { Vehicle = vehicle });
                        App.MainViewModel.SelectedVehicle = vehicle;
                        break;

                    case Mode.Edit:
                        if(vehicle.OdometerUnit != _originalVehicle.OdometerUnit)
                        {
                            MessageBox.Show("Changing the odometer unit is not allowed when editing a vehicle.");
                            return;
                        }

                        Vehicle.Save(vehicle, _originalVehicle.Id);
                        //move the entries over to the new intance of edited vehicle
                        foreach (var fillupEntry in _originalVehicle.Entries)
                        {
                            vehicle.Entries.Add(fillupEntry);
                        }

                        App.VehicleStore[vehicle.Id] = vehicle;
                        App.MainViewModel.VehiclePages
                            .Where(vp => vp.Vehicle.Id == vehicle.Id) 
                            .Single().Vehicle = vehicle;
                        App.MainViewModel.SelectedVehicle = vehicle;
                        break;
                    default:
                        throw new Exception("should never get here.");
                }
                
            }
            catch (PetroledgerValidationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            catch (DuplicateVehicleLabelException)
            {
                MessageBox.Show("A vehicle with this name already exists.");
                return;
            }


            NavigationService.GoBack();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}