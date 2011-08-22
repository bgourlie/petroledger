using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Petroledger.Data.Analysis;
using Petroledger.Data.Model;

namespace Petroledger.ViewModels
{
    public class ViewEntriesViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<IFillupEntryListItem> _listItems;
        private Vehicle _vehicle;
        
        public Vehicle Vehicle
        {
            get { return _vehicle; }
            set
            {
                _vehicle = value;
                _listItems = new ObservableCollection<IFillupEntryListItem>();

                List<FillupEntryAnalysis> fillupAnalysises = App.GetVehicleAnalysis(_vehicle).FillupEntryAnalyses;

                for (int i = _vehicle.Entries.Count - 1; i > 0; i--)
                {
                    FillupEntry entry = _vehicle.Entries[i];

                    FillupEntryAnalysis associatedAnalysis = (from a in fillupAnalysises
                                                              where a.CalculatedEntry.Equals(entry)
                                                              select a).FirstOrDefault();

                    ListItems.Add(new FillupEntryListItemViewModel
                                      {EntryAnalysis = associatedAnalysis, FillupEntry = entry});

                    if (entry.PreviousEntryMissed)
                        ListItems.Add(new MissedEntryListItemViewModel
                                          {
                                              MissedFromDate = _vehicle.Entries[i - 1].EntryDate,
                                              MissedToDate = entry.EntryDate
                                          });
                }

                if (_vehicle.Entries.Count > 0)
                {
                    ListItems.Add(new FillupEntryListItemViewModel
                                      {EntryAnalysis = null, FillupEntry = _vehicle.Entries[0]});
                }

                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("ListItems"));
            }
        }

        public ObservableCollection<IFillupEntryListItem> ListItems
        {
            get { return _listItems; }
            set { _listItems = value; }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}