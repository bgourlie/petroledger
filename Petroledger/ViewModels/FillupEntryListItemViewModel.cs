using System;
using System.Windows;
using Petroledger.Data.Analysis;
using Petroledger.Data.Model;

namespace Petroledger.ViewModels
{
    public class FillupEntryListItemViewModel : IFillupEntryListItem
    {
        public FillupEntry FillupEntry { get; set; }
        public FillupEntryAnalysis EntryAnalysis { get; set; }

        public string OdometerReadingString
        {
            get { return FillupEntry.OdometerReading.ToString("######.#"); }
        }

        public string EntryDateString
        {
            get { return FillupEntry.EntryDate.ToShortDateString(); }
        }

        public string MissedEntryDatesString
        {
            get { return null; }
        }

        public string MissedEntryLabel
        {
            get { return null; }
        }

        public string PricePerUnitString
        {
            get { return FillupEntry.PricePerUnit.ToString("C3"); }
        }

        public string FillAmountString
        {
            get { return FillupEntry.FillAmount.ToString("F3") + " " + FillupEntry.PumpUnit.GetVolumeAbbreviation(); }
        }

        public string FuelEfficiencyString
        {
            get
            {

                
                return EntryAnalysis != null
                           ? EntryAnalysis.FuelEfficiency.GetEfficiencyString(EntryAnalysis.DistanceUnit,
                                                                              EntryAnalysis.VolumeUnit)
                           : "na";
            }
        }

        public bool IsPreviousEntryMissed
        {
            get { return FillupEntry.PreviousEntryMissed; }
        }

        public bool IsTankNotToppedOff
        {
            get { return FillupEntry.WasNotToppedOff; }
        }

        public Visibility FillupEntryVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility MissedEntryLabelVisibility
        {
            get { return Visibility.Collapsed; }
        }

        public Visibility EditEntryContextMenuItemVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility DeleteEntryContextMenuItemVisibility
        {
            get { return Visibility.Visible; }
        }

        public Visibility InsertEntryContextMenuItemVisibility
        {
            get { return Visibility.Collapsed; }
        }
    }
}