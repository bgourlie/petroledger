using System;
using Petroledger.Data;
using Petroledger.Data.Model;

namespace Petroledger.ViewModels
{
    public class AddOrEditFillupEntryViewModel
    {
        public Vehicle Vehicle { get; set; }
        public FillupEntry FillupEntry { get; set; }
        
        public AddOrEditFillupEntry.Mode Mode { get; set; }
        public DateTime InsertDateFrom { get; set; }
        public DateTime InsertDateTo { get; set; }
        public long OriginalEntryTicks { get; set; }

        public string MissedEntryLabel
        {
            get
            {
                return Mode == AddOrEditFillupEntry.Mode.Insert
                           ? string.Format("missed between {0:d} and {1:d}", InsertDateFrom, InsertDateTo)
                           : null;
            }
        }

        public string PricePerUnitLabel
        {
            get { return string.Format("price per " + UnitOfMeasure.DefaultVolumeUnit.ToString().ToLower()); }
        }

        public string PageHeader
        {
            get
            {
                switch (Mode)
                {
                    case AddOrEditFillupEntry.Mode.New:
                        return "new entry";
                    case AddOrEditFillupEntry.Mode.Edit:
                        return "edit entry";
                    case AddOrEditFillupEntry.Mode.Insert:
                        return "insert entry";
                    default:
                        throw new Exception("Unknown mode.");
                }
            }
        }
    }
}