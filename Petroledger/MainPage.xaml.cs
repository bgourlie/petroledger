using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Shell;
using Petroledger.Data.Model;
using Petroledger.Extensions;
using Petroledger.SampleData;
using Petroledger.ViewModels;

namespace Petroledger
{
    public partial class MainPage
    {
        private readonly BackgroundWorker _backgroundWorker;
        private bool _isNewInstance;
        private bool _dataIsLoading;
        private const int APPBAR_MENU_ADD_VEHICLE = 0;
        private const int APPBAR_MENU_EDIT_VEHICLE = 1;
        private const int APPBAR_MENU_DELETE_VEHICLE = 2;
        private const int APPBAR_BUTTON_VIEW_ENTRIES = 1;
        private const int APPBAR_BUTTON_RECORD_ENTRY = 0;
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.MainViewModel;
            _isNewInstance = true;
            
            Debug.WriteLine("MainPage Constructor Called.  Checking if vehicle data is loaded...");
            if (!App.IsDataLoaded)
            {
                Debug.WriteLine("Vehicle data is not loaded.  Loading asyncronously...");
                _dataIsLoading = true;
                _backgroundWorker = new BackgroundWorker();
                _backgroundWorker.DoWork += App.LoadData;
                _backgroundWorker.RunWorkerCompleted += OnDataLoadingComplete;
                _backgroundWorker.RunWorkerAsync();
            }
            else
            {
                Debug.WriteLine("Data already loaded.");
                App.MainViewModel.VehiclePages.CollectionChanged += OnVehiclePagesCollectionChanged;
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _isNewInstance = false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Debug.WriteLine("MainPage.OnNavigatedTo()");

            if (_dataIsLoading)
            {
                Debug.WriteLine("\tData is being loaded asyncronously.  Nothing to do here.");
                return;
            }

            if(_isNewInstance)
            {
                Debug.WriteLine("\tIs new page instance.  Calling InitializePage().");
                InitializePage();
            }else
            {
                Debug.WriteLine("\tNot a new page instance, displaying the SelectedVehicle.");
                var viewModel = (MainViewModel) DataContext;
                if (viewModel.SelectedVehicle != null && PivotCars.SelectedItem != viewModel.SelectedVehicle)
                {
                    var vehicleSummaryViewModel =
                        viewModel.VehiclePages.Where(vp => vp.Vehicle.Id == viewModel.SelectedVehicle.Id).Single();
                    PivotCars.SelectedItem = vehicleSummaryViewModel;
                    vehicleSummaryViewModel.UpdateSummary();
                }
                    
                UpdateMenuItems();
            }
        }

        private void OnDataLoadingComplete(object sender, RunWorkerCompletedEventArgs e)
        {
            Debug.WriteLine("Finished loading data from storage asyncronously.  Invoking InitializePage().");
            _dataIsLoading = false;
            Dispatcher.BeginInvoke(InitializePage);
        }

        private void InitializePage()
        {
            Debug.WriteLine("InitializePage() called.");
            var mainDataContext = (MainViewModel)DataContext;

            foreach (var vehicle in App.VehicleStore.Values)
            {
                mainDataContext.VehiclePages.Add(new VehicleSummaryViewModel { Vehicle = vehicle });
            }

            //if we're coming to the page and the SelectedVehicle isn't already set,
            //set it to the first vehicle in the VehicleStore if there are any
            if (mainDataContext.SelectedVehicle == null && App.VehicleStore.Count > 0)
                mainDataContext.SelectedVehicle = App.VehicleStore.Values.First();
            
            if(mainDataContext.SelectedVehicle != null)
                PivotCars.SelectedItem =
                    mainDataContext.VehiclePages
                    .Where(vp => vp.Vehicle == mainDataContext.SelectedVehicle)
                    .Single();

            App.MainViewModel.VehiclePages.CollectionChanged +=
                OnVehiclePagesCollectionChanged;
            OnPageLoaded(null, null);
            OnVehiclePagesCollectionChanged(null, null);

            if (!App.SampleVehicleDialogWasShown)
            {
                SampleVehicleDialog.IsOpen = Settings.ShowSampleVehiclePopupOnStart;
                App.SampleVehicleDialogWasShown = true;
            }

        }

        private void NewVehicleClickEventHandler(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AddOrEditVehicle.xaml?mode=new", UriKind.Relative));
        }

        private void NewEntryClickEventHandler(object sender, EventArgs e)
        {
            var viewModel = (MainViewModel) DataContext;
            if (viewModel.SelectedVehicle == null)
            {
                MessageBox.Show("You must add a vehicle before you can record a fill-up.");
                return;
            }
            string uriString = string.Format(
                "/AddOrEditFillupEntry.xaml?mode={0}&vehicleId={1}", AddOrEditFillupEntry.Mode.New,
                viewModel.SelectedVehicle.Id);

            NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
        }

        private void OnPivotItemChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = (MainViewModel) DataContext;
            var vehicleViewModel = e.AddedItems[0] as VehicleSummaryViewModel;
            
            if(vehicleViewModel == null)
            {
                viewModel.SelectedVehicle = null;
                return;
            }

            viewModel.SelectedVehicle = vehicleViewModel.Vehicle;
            if (!vehicleViewModel.IsSummaryCurrent) vehicleViewModel.UpdateSummary(); ;
            UpdateMenuItems();
        }

        private void ViewEntryClickEventHandler(object sender, EventArgs e)
        {
            var viewModel = (MainViewModel) DataContext;
            //should we show a messagebox here instead of just exiting?  Nah, they'll understand.
            if (viewModel.SelectedVehicle == null || viewModel.SelectedVehicle.Entries.Count == 0) return;

            NavigationService.Navigate(new Uri("/ViewEntries.xaml?vehicleId=" + viewModel.SelectedVehicle.Id,
                                               UriKind.Relative));
        }

        private void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            loadingProgressBar.Visibility = !App.IsDataLoaded ? Visibility.Visible : Visibility.Collapsed;

            PivotCars.Visibility = App.IsDataLoaded && App.VehicleStore.Count > 0
                                       ? Visibility.Visible
                                       : Visibility.Collapsed;

            LayoutNoVehicles.Visibility = App.IsDataLoaded && App.VehicleStore.Count == 0
                                              ? Visibility.Visible
                                              : Visibility.Collapsed;

            var viewModel = (MainViewModel) DataContext;
            //refresh the current pivots stats if they are stale
            if (viewModel.SelectedVehicle == null) return;
            var selectedVehicleViewModel =
                viewModel.VehiclePages.Where(vp => vp.Vehicle == viewModel.SelectedVehicle).Single();

            PivotCars.SelectedItem = selectedVehicleViewModel;
            if (!selectedVehicleViewModel.IsSummaryCurrent) selectedVehicleViewModel.UpdateSummary();
            
        }

        public void OnVehiclePagesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            PivotCars.Visibility = App.IsDataLoaded && App.VehicleStore.Count > 0
                                       ? Visibility.Visible
                                       : Visibility.Collapsed;

            LayoutNoVehicles.Visibility = App.IsDataLoaded && App.VehicleStore.Count == 0
                                              ? Visibility.Visible
                                              : Visibility.Collapsed;

            UpdateMenuItems();
        }

        private void DeleteVehicleClickEventHandler(object sender, EventArgs e)
        {
            var viewModel = (MainViewModel) DataContext;
            if (viewModel.SelectedVehicle == null) return;
            var confirmDelete = MessageBox.Show(string.Format("are you sure you want to delete {0} and all {1} entries?  choose 'ok' to delete.", viewModel.SelectedVehicle.VehicleName, viewModel.SelectedVehicle.Entries.Count), "", MessageBoxButton.OKCancel);
            if (confirmDelete == MessageBoxResult.Cancel) return;
            var confirmAgain = MessageBox.Show(string.Format("last chance to back out!  choose 'cancel' to abort."), "", MessageBoxButton.OKCancel);
            if (confirmAgain == MessageBoxResult.Cancel) return;
            App.DeleteVehicle(viewModel.SelectedVehicle);
        }

        private void UpdateMenuItems()
        {
            var mainViewModel = (MainViewModel) DataContext;
            var deleteVehicleMenuItem = (ApplicationBarMenuItem)ApplicationBar.MenuItems[APPBAR_MENU_DELETE_VEHICLE];
            var editVehicleMenuItem = (ApplicationBarMenuItem)ApplicationBar.MenuItems[APPBAR_MENU_EDIT_VEHICLE];
            var viewEntriesMenuButton = (ApplicationBarIconButton)ApplicationBar.Buttons[APPBAR_BUTTON_VIEW_ENTRIES];
            var recordEntryMenuButton = (ApplicationBarIconButton)ApplicationBar.Buttons[APPBAR_BUTTON_RECORD_ENTRY];
            deleteVehicleMenuItem.IsEnabled = PivotCars.Items.Count > 0;
            editVehicleMenuItem.IsEnabled = PivotCars.Items.Count > 0;
            recordEntryMenuButton.IsEnabled = PivotCars.Items.Count > 0;;
            viewEntriesMenuButton.IsEnabled = PivotCars.Items.Count > 0 && mainViewModel.SelectedVehicle != null && mainViewModel.SelectedVehicle.Entries.Count > 0;
        }

        private void EditVehicleClickEventHandler(object sender, EventArgs e)
        {
            var viewModel = (MainViewModel) DataContext;
            if (viewModel.SelectedVehicle == null) return;
            NavigationService.Navigate(new Uri("/AddOrEditVehicle.xaml?mode=edit&vehicleId=" + viewModel.SelectedVehicle.Id, UriKind.Relative));
        }

        private void BtnSampleVehicleDialog(object sender, RoutedEventArgs e)
        {
            bool doNotShowDialog = false;

            if (sender == BtnYes)
            {
                Vehicle vehicle = HondaTestData.SampleVehicle();
                Vehicle.Save(vehicle);
                App.VehicleStore.Add(vehicle.Id, vehicle);

                foreach (var fillupEntry in vehicle.Entries)
                {
                    vehicle.SaveEntry(fillupEntry);
                }
                App.MainViewModel.VehiclePages.Add(new VehicleSummaryViewModel {Vehicle = vehicle});
                App.MainViewModel.SelectedVehicle = vehicle;
                doNotShowDialog = true;
            }

            Settings.ShowSampleVehiclePopupOnStart = doNotShowDialog 
                ? false 
                : ChkDontShowSampleVehicleDialog.IsChecked.HasValue && !ChkDontShowSampleVehicleDialog.IsChecked.Value;

            SampleVehicleDialog.IsOpen = false;
        }
    }
}