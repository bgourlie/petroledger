using System.Windows;

namespace Petroledger.ViewModels
{
    public interface IFillupEntryListItem
    {
        string EntryDateString { get; }
        string MissedEntryDatesString { get; }
        string MissedEntryLabel { get; }
        string FuelEfficiencyString { get; }
        string PricePerUnitString { get; }
        string FillAmountString { get; }
        bool IsPreviousEntryMissed { get; }
        bool IsTankNotToppedOff { get; }

        Visibility FillupEntryVisibility { get; }
        Visibility MissedEntryLabelVisibility { get; }
        Visibility EditEntryContextMenuItemVisibility { get; }
        Visibility DeleteEntryContextMenuItemVisibility { get; }
        Visibility InsertEntryContextMenuItemVisibility { get; }
    }
}