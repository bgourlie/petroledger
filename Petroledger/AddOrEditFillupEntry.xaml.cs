using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Petroledger.Data;
using Petroledger.Data.Model;
using Petroledger.Exceptions;
using Petroledger.Extensions;
using Petroledger.ViewModels;

namespace Petroledger
{
    public partial class AddOrEditFillupEntry
    {
        //stores the reference to the original entry 
        //when in Edit mode

        public enum Mode
        {
            New,
            Edit,
            Insert
        }


        private bool _isNewInstance;
        private FillupEntry _originalEntry;
        

        public AddOrEditFillupEntry()
        {
            InitializeComponent();
            _isNewInstance = true;
            if (DesignerProperties.IsInDesignTool)
            {
                DataContext = new AddOrEditFillupEntryViewModel
                                  {
                                      FillupEntry =
                                          new FillupEntry
                                              {
                                                  EntryDate = DateTime.Now, 
                                                  PumpUnit = UnitOfMeasure.DefaultVolumeUnit
                                              }
                                  };
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            StateUtils.PreserveState(State, TxtFillAmount);
            StateUtils.PreserveState(State, TxtOdometerReading);
            StateUtils.PreserveState(State, TxtPricePerUnit);
            StateUtils.PreserveState(State, EntryDatePicker);
            StateUtils.PreserveState(State, chkNotToppedOff);
            StateUtils.PreserveState(State, chkMissedPreviousEntry);
            State["StatePreserved"] = true;
            _isNewInstance = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            if (!_isNewInstance) return;
            
            string mode;
            string vehicleIdString;
            NavigationContext.QueryString.TryGetValue("mode", out mode);
            NavigationContext.QueryString.TryGetValue("vehicleId", out vehicleIdString);
            int vehicleId = int.Parse(vehicleIdString);
            var entryMode = (Mode) (Enum.Parse(typeof (Mode), mode, true));
            var viewModel = new AddOrEditFillupEntryViewModel {Vehicle = App.VehicleStore[vehicleId], Mode = entryMode};
            
            switch (entryMode)
            {
                case Mode.New:
                    viewModel.FillupEntry = new FillupEntry
                                                {
                                                    EntryDate = DateTime.Now,
                                                    PumpUnit = UnitOfMeasure.DefaultVolumeUnit,
                                                    OdometerUnit = viewModel.Vehicle.OdometerUnit,
                                                };
                    break;

                case Mode.Edit:
                    string entryTicksString;
                    NavigationContext.QueryString.TryGetValue("entryTicks", out entryTicksString);
                    long entryTicks = long.Parse(entryTicksString);
                    viewModel.OriginalEntryTicks = entryTicks;
                    _originalEntry = viewModel.Vehicle.Entries
                        .Where(entry => entry.EntryDate.Ticks == entryTicks)
                        .Single();

                    viewModel.FillupEntry = FillupEntry.Clone(_originalEntry);
                    break;

                case Mode.Insert:
                    string fromDateString;
                    string toDateString;
                    NavigationContext.QueryString.TryGetValue("fromDate", out fromDateString);
                    NavigationContext.QueryString.TryGetValue("toDate", out toDateString);
                    var fromDate = new DateTime(long.Parse(fromDateString));
                    var toDate = new DateTime(long.Parse(toDateString));
                    viewModel.InsertDateFrom = fromDate;
                    viewModel.InsertDateTo = toDate;

                    viewModel.FillupEntry = new FillupEntry
                                                {
                                                    PumpUnit = UnitOfMeasure.DefaultVolumeUnit,
                                                    OdometerUnit = viewModel.Vehicle.OdometerUnit,
                                                    EntryDate = DateTime.Today
                                                };
                    break;
                default:
                    throw new Exception("We should never get here.");
            }

            DataContext = viewModel;

            //restore previous state
            if (!State.ContainsKey("StatePreserved")) return;
            StateUtils.RestoreState(State, TxtFillAmount, String.Empty);
            StateUtils.RestoreState(State, TxtOdometerReading, String.Empty);
            StateUtils.RestoreState(State, TxtPricePerUnit, String.Empty);
            StateUtils.RestoreState(State, EntryDatePicker, DateTime.Now);
            StateUtils.RestoreState(State, chkNotToppedOff, false);
            StateUtils.RestoreState(State, chkMissedPreviousEntry, false);

        }

        private void OnSaveClicked(object sender, EventArgs e)
        {
            // ReSharper disable PossibleNullReferenceException
            //for update source because textbox's arent updated until they lose focus
            TxtFillAmount.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TxtOdometerReading.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TxtPricePerUnit.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            // ReSharper restore PossibleNullReferenceException
            var viewModel = (AddOrEditFillupEntryViewModel)DataContext;

            try
            {
                viewModel.FillupEntry.Validate(viewModel.Vehicle);

                switch (viewModel.Mode)
                {
                    case Mode.Insert:
                        if (viewModel.FillupEntry.EntryDate < viewModel.InsertDateFrom ||
                            viewModel.FillupEntry.EntryDate > viewModel.InsertDateTo)
                        {
                            MessageBox.Show(String.Format("The Entry Date must be between {0:d} and {1:d}.",
                                                          viewModel.InsertDateFrom,
                                                          viewModel.InsertDateTo));
                            return;
                        }
                        viewModel.Vehicle.SaveEntry(viewModel.FillupEntry);
                        viewModel.Vehicle.InsertEntry(viewModel.FillupEntry);
                        //remove the "previous entry missed" flag from the subsequent entry
                        int subsequentEntryIndex = viewModel.Vehicle.Entries.IndexOf(viewModel.FillupEntry) + 1;
                        //index should never be out of bounds (by convention, cannot "insert" at the top of the entry stack)
                        var subsequentEntry = viewModel.Vehicle.Entries[subsequentEntryIndex];
                        subsequentEntry.PreviousEntryMissed = false;
                        viewModel.Vehicle.UpdateExistingEntry(subsequentEntry, subsequentEntry.EntryDate.Ticks);
                        break;

                    case Mode.New:
                        viewModel.Vehicle.SaveEntry(viewModel.FillupEntry);
                        viewModel.Vehicle.InsertEntry(viewModel.FillupEntry);
                        break;
                    case Mode.Edit:
                        //remove the original entry and replace it with the updated one
                        viewModel.Vehicle.Entries.Remove(_originalEntry);
                        viewModel.Vehicle.InsertEntry(viewModel.FillupEntry);
                        viewModel.Vehicle.UpdateExistingEntry(viewModel.FillupEntry, viewModel.OriginalEntryTicks);
                        break;
                    default:
                        throw new Exception("we should never get here.");
                }
            }
            catch (PetroledgerValidationException ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            App.InvalidateAnalysisCache(viewModel.Vehicle);
            NavigationService.GoBack();
        }

        private void OnCancelClicked(object sender, EventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}