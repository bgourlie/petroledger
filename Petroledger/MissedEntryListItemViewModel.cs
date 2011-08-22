using System;
using System.Windows;

namespace Petroledger.ViewModels
{
    public class MissedEntryListItemViewModel : IFillupEntryListItem
    {
        public DateTime MissedFromDate { get; set; }
        public DateTime MissedToDate { get; set; }

        #region IFillupEntryListItem Members

        public string EntryDateString
        {
            get { return null; }
        }

        public string MissedEntryDatesString
        {
            get { return string.Format("between {0:d} and {1:d}", MissedFromDate, MissedToDate); }
        }

        public string MissedEntryLabel
        {
            get { return "missed entry"; }
        }

        public string FuelEfficiencyString
        {
            get { return null; }
        }

        public string PricePerUnitString
        {
            get { return null; }
        }

        public string FillAmountString
        {
            get { return null; }
        }

        public bool IsPreviousEntryMissed
        {
            get { return false; }
        }

        public bool IsTankNotToppedOff
        {
            get { return false; }
        }

        public Visibility FillupEntryVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public Visibility MissedEntryLabelVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility EditEntryContextMenuItemVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public Visibility DeleteEntryContextMenuItemVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public Visibility InsertEntryContextMenuItemVisibility
        {
            get { return Visibility.Visible; }
        }

        #endregion
    }
}