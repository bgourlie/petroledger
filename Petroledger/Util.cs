using System.IO.IsolatedStorage;
using Petroledger.Data;

namespace Petroledger
{
    public static class Util
    {
        public const string YEAR_REGEX = @"^(?:19|20)([0-9][0-9])$";

        public static void RecursiveDeleteDirectory(string directory, IsolatedStorageFile store)
        {
            
            foreach (var fileName in store.GetFileNames(directory + "\\*"))
            {
                store.DeleteFile(directory + "\\" + fileName);
            }

            foreach (var directoryName in store.GetDirectoryNames(directory + "\\*"))
            {
                RecursiveDeleteDirectory(directory + "\\" + directoryName, store);
            }

            store.DeleteDirectory(directory);
        }

        public static string GetEfficiencyString(this double efficiency, UnitOfMeasure.Distance distanceUnit,
                                                 UnitOfMeasure.Volume volumeUnit)
        {
            //standard imperial efficiency abbreviation
            if (distanceUnit == UnitOfMeasure.Distance.Mile &&
                volumeUnit == UnitOfMeasure.Volume.Gallon)
                return string.Format("{0:###.#} mpg", efficiency);

            //standard metric efficiency abbreviation
            if (distanceUnit == UnitOfMeasure.Distance.Kilometer &&
                volumeUnit == UnitOfMeasure.Volume.Liter)
                return string.Format("{0:###.#} km/L", efficiency);
            ;

            //for any case where imperial and metric units are used for either volume or distance
            return string.Format("{0:###.#} {1} per {2}", efficiency, distanceUnit.ToString().ToLower(),
                                 volumeUnit.ToString().ToLower());
        }

        public static string GetVolumeAbbreviation(this UnitOfMeasure.Volume volumeUnit)
        {
            return volumeUnit == UnitOfMeasure.Volume.Liter
                       ? "ℓ"
                       : "gal";
        }
    }
}