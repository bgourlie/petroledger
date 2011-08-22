using System.Globalization;

namespace Tools
{
    public static class UnitOfMeasure
    {

        public static Distance DefaultDistanceUnit
        {
            get { return RegionInfo.CurrentRegion.IsMetric ? Distance.Kilometer : Distance.Mile; }
        }

        public static Volume DefaultVolumeUnit
        {
            get { return RegionInfo.CurrentRegion.IsMetric ? Volume.Liter : Volume.Gallon; }
        }

        public enum Distance : byte
        {
            Kilometer,
            Mile
        }

        public enum Volume : byte
        {
            Liter,
            Gallon
        }
    }
}