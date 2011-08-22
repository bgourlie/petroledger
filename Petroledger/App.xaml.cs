using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Petroledger.Data;
using Petroledger.Data.Analysis;
using Petroledger.Data.Model;
using Petroledger.Extensions;
using Petroledger.ViewModels;

namespace Petroledger
{
    public partial class App : Application
    {
        public static Action<Vehicle> AnalysisCacheInvalidated;
        public static bool SampleVehicleDialogWasShown;
        public static Dictionary<int, Vehicle> VehicleStore = new Dictionary<int, Vehicle>();

        private static readonly Dictionary<Vehicle, VehicleAnalysis> __analysisCache =
            new Dictionary<Vehicle, VehicleAnalysis>();

        private static readonly Dictionary<Vehicle, bool> __analysisCacheIsStale = new Dictionary<Vehicle, bool>();
        public static readonly MainViewModel MainViewModel = new MainViewModel();

        public App()
        {
            // Note that exceptions thrown by ApplicationBarItem.Click will not get caught here.
            UnhandledException += Application_UnhandledException;

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();
        }

        public static bool IsDataLoaded { get; private set; }


        public PhoneApplicationFrame RootFrame { get; private set; }

        public static void InvalidateAnalysisCache(Vehicle vehicle)
        {
            __analysisCacheIsStale[vehicle] = true;
            if (AnalysisCacheInvalidated == null) return;
            AnalysisCacheInvalidated(vehicle);
        }

        public static VehicleAnalysis GetVehicleAnalysis(Vehicle vehicle)
        {
            Debug.WriteLine("GetVehicleAnalysis({0}) called", vehicle.VehicleName);
            if (!__analysisCache.ContainsKey(vehicle))
            {
                Debug.WriteLine("Analysis doesn't exist in the dict, generating and adding it.");
                __analysisCache.Add(vehicle,
                                    vehicle.GenerateVehicleAnalysis(UnitOfMeasure.DefaultDistanceUnit,
                                                                    UnitOfMeasure.DefaultVolumeUnit));

                if (!__analysisCacheIsStale.ContainsKey(vehicle))
                    __analysisCacheIsStale.Add(vehicle, false);
                else
                    __analysisCacheIsStale[vehicle] = false;

                return __analysisCache[vehicle];
            }

            if (__analysisCacheIsStale[vehicle])
            {
                Debug.WriteLine("Analysis for {0} is stale.  Regenerating.", vehicle.VehicleName);
                __analysisCache[vehicle] = vehicle.GenerateVehicleAnalysis(UnitOfMeasure.DefaultDistanceUnit,
                                                                           UnitOfMeasure.DefaultVolumeUnit);
                __analysisCacheIsStale[vehicle] = false;
            }

            return __analysisCache[vehicle];
        }

        public static void LoadData(object sender, DoWorkEventArgs doWorkEventArgs)
        {
            Debug.WriteLine("LoadData() Called.");
            foreach (var vehicle in Vehicle.LoadVehiclesFromStorage())
            {
                vehicle.LoadEntriesFromStorage();
                VehicleStore.Add(vehicle.Id, vehicle);
                __analysisCache.Add(vehicle,
                                    vehicle.GenerateVehicleAnalysis(UnitOfMeasure.DefaultDistanceUnit,
                                                                    UnitOfMeasure.DefaultVolumeUnit));
                __analysisCacheIsStale.Add(vehicle, false);
            }

            Debug.WriteLine("LoadData() complete.");
            IsDataLoaded = true;
        }


        private static void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(e.Exception.StackTrace);
                Debugger.Break();
            }
        }

        private static void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (Debugger.IsAttached)
            {
                Debug.WriteLine(e.ExceptionObject.StackTrace);
                Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool _phoneApplicationInitialized;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (_phoneApplicationInitialized) return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;
            _phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion

        private void PhoneApplicationService_Activated(object sender, ActivatedEventArgs e)
        {
            Debug.WriteLine("Application Activated");

            if (PhoneApplicationService.Current.State.ContainsKey("SampleVehicleDialogShown"))
                SampleVehicleDialogWasShown = true;

            // Check to see if the key for the application state data is in the State dictionary.);
            if (!PhoneApplicationService.Current.State.ContainsKey("vehicleStore")) return;

            Debug.WriteLine("Found previous state - restoring VehicleStore from persisted data.");

            /*
                * For whatever reason (perhaps some serialization magic behind the scenes) when retrieving,
                * the dictionary from persisted storage, they are reconstructed which sets their Id to -1.
                * Normally this is set to the correct id when the vehicle is being loaded from IsolatedStorage,
                * but that is not happing here when we load it from the persisted state dictionary.  So, we
                * use this Vehicle.AssignId method to do it using the dictionary key.
                */
            VehicleStore = (Dictionary<int, Vehicle>) PhoneApplicationService.Current.State["vehicleStore"];

            foreach (var kvp in VehicleStore)
            {
                Debug.WriteLine("Restored Vehicle: {0}: {1}", kvp.Key, kvp.Value.VehicleName);
                Vehicle.AssignId(kvp.Value, kvp.Key);
            }
            var selectedVehicleId = PhoneApplicationService.Current.State["selectedVehicleId"] as int?;

            if (selectedVehicleId.HasValue)
            {
                MainViewModel.SelectedVehicle = VehicleStore.Values
                    .Where(v => v.Id == selectedVehicleId.Value)
                    .SingleOrDefault();
            }

            IsDataLoaded = true;
            
        }

        private void PhoneApplicationService_Deactivated(object sender, DeactivatedEventArgs e)
        {
            Debug.WriteLine("Application deactivating.  Saving state to persisted data.");
            PhoneApplicationService.Current.State["SampleVehicleDialogShown"] = true;
            PhoneApplicationService.Current.State["vehicleStore"] = VehicleStore;
            PhoneApplicationService.Current.State["selectedVehicleId"] = MainViewModel.SelectedVehicle != null
                                                                             ? new int?(MainViewModel.SelectedVehicle.Id)
                                                                             : null;
        }

        public static void DeleteVehicle(Vehicle vehicle)
        {
            if(!VehicleStore.ContainsKey(vehicle.Id))
                throw new InvalidOperationException("Vehicle must exist in vehicle store.");

            VehicleStore.Remove(vehicle.Id);
            if(__analysisCache.ContainsKey(vehicle)) __analysisCache.Remove(vehicle);
            if(__analysisCache.ContainsKey(vehicle)) __analysisCacheIsStale.Remove(vehicle);
            
            using(var store = IsolatedStorageFile.GetUserStoreForApplication())
            {
                Util.RecursiveDeleteDirectory(vehicle.Id.ToString(), store);
            }

            MainViewModel.VehiclePages.Remove(MainViewModel.VehiclePages
                .Where(vp => vp.Vehicle.Equals(vehicle)).Single());

            if(vehicle == MainViewModel.SelectedVehicle)
            {
                MainViewModel.SelectedVehicle = VehicleStore.Count > 0
                                                    ? VehicleStore.Values.First()
                                                    : null;
            }
        }
    }

 
}