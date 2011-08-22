using System.Globalization;

namespace Petroledger.Data
{
    public static class UnitOfMeasure
    {
        #region Distance enum

        public enum Distance : byte
        {
            Kilometer,
            Mile
        }

        #endregion

        #region Volume enum

        public enum Volume : byte
        {
            Liter,
            Gallon
        }

        #endregion

        public static Distance DefaultDistanceUnit
        {
            get { return RegionInfo.CurrentRegion.IsMetric ? Distance.Kilometer : Distance.Mile; }
        }

        public static Volume DefaultVolumeUnit
        {
            get { return RegionInfo.CurrentRegion.IsMetric ? Volume.Liter : Volume.Gallon; }
        }
    }
}