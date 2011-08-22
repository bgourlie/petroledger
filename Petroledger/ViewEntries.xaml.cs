using System;
using System.Windows;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Petroledger.Extensions;
using Petroledger.ViewModels;

namespace Petroledger
{
    public partial class ViewEntries : PhoneApplicationPage
    {
        private bool _isNewInstance;

        // Constructor
        public ViewEntries()
        {
            InitializeComponent();
            _isNewInstance = true;
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            System.Diagnostics.Debug.WriteLine("ViewEntries.OnNavigatedTo()");

            string selectedIndex;
            if (!NavigationContext.QueryString.TryGetValue("vehicleId", out selectedIndex)) return;
            int id = int.Parse(selectedIndex);
            DataContext = new ViewEntriesViewModel {Vehicle = App.VehicleStore[id]};
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _isNewInstance = false;
        }

        private void OnEditEntryMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;
            var viewModel = (IFillupEntryListItem) menuItem.Tag;
            var viewEntriesViewModel = (ViewEntriesViewModel) DataContext;
            var entryListItemViewModel = (FillupEntryListItemViewModel) viewModel;

            string uriString = string.Format("/AddOrEditFillupEntry.xaml?mode={0}&vehicleId={1}&entryTicks={2}",
                                             AddOrEditFillupEntry.Mode.Edit,
                                             viewEntriesViewModel.Vehicle.Id,
                                             entryListItemViewModel.FillupEntry.EntryDate.Ticks);

            NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
        }

        private void OnInsertEntryMenuItemClicked(object sender, RoutedEventArgs e)
        {
            var menuItem = (MenuItem) sender;
            var viewModel = (IFillupEntryListItem) menuItem.Tag;
            var viewEntriesViewModel = (ViewEntriesViewModel) DataContext;
            var missedEntryItem = (MissedEntryListItemViewModel) viewModel;

            string uriString =
                string.Format("/AddOrEditFillupEntry.xaml?mode={0}&vehicleId={1}&fromDate={2}&toDate={3}",
                              AddOrEditFillupEntry.Mode.Insert, viewEntriesViewModel.Vehicle.Id,
                              missedEntryItem.MissedFromDate.Ticks,
                              missedEntryItem.MissedToDate.Ticks);

            NavigationService.Navigate(new Uri(uriString, UriKind.Relative));
        }

        private void OnDeleteEntryMenuItemClicked(object sender, RoutedEventArgs e)
        {
            if(MessageBox.Show("are you sure you want to delete this entry?  choose 'ok' to delete.", "", MessageBoxButton.OKCancel) != MessageBoxResult.OK)
                return;

            var menuItem = (MenuItem) sender;
            var entryViewModel = (IFillupEntryListItem) menuItem.Tag;
            var pageViewModel = (ViewEntriesViewModel) DataContext;
            var entryItemViewModel = (FillupEntryListItemViewModel) entryViewModel;
            pageViewModel.Vehicle.DeleteFillupEntry(entryItemViewModel.FillupEntry);
            pageViewModel.Vehicle.Entries.Remove(entryItemViewModel.FillupEntry);
            int itemViewModelIndex = pageViewModel.ListItems.IndexOf(entryItemViewModel);
            pageViewModel.ListItems.Remove(entryItemViewModel);

            if (entryItemViewModel.FillupEntry.PreviousEntryMissed)
                pageViewModel.ListItems.RemoveAt(itemViewModelIndex);

            App.InvalidateAnalysisCache(pageViewModel.Vehicle);
            if(pageViewModel.Vehicle.Entries.Count == 0) NavigationService.GoBack();

        }
    }
}