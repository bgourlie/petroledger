using System.Collections.Generic;

namespace Tools
{
    public partial class Vehicle
    {
        private readonly List<FillupEntry> _entries;
        public string Model { get; set; }
        public int Year { get; set; }
        public string Make { get; set; }
        public string Label { get; set; }
        public int Id { get; private set; }

        public UnitOfMeasure.Distance OdometerUnit { get; set; }

        public List<FillupEntry> Entries
        {
            get { return _entries; }
        }
        
        public Vehicle()
        {
            Id = -1;
            _entries = new List<FillupEntry>();
        }
    }
}