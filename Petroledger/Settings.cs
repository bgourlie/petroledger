using System.IO.IsolatedStorage;

namespace Petroledger
{
    public static class Settings
    {
        private static readonly IsolatedStorageSettings __isolatedStorageSettings;
        private const string SHOW_SAMPLE_VEHICLE_POPUP_ON_START = "show_sample_vehicle_popup_on_start";

        private static bool _showSampleVehiclePopupOnStart;

        static Settings()
        {
            __isolatedStorageSettings = IsolatedStorageSettings.ApplicationSettings;
        }

        public static bool  ShowSampleVehiclePopupOnStart
        {
            get
            {
                if (__isolatedStorageSettings.Contains(SHOW_SAMPLE_VEHICLE_POPUP_ON_START))
                {

                    _showSampleVehiclePopupOnStart =
                        (bool) __isolatedStorageSettings[SHOW_SAMPLE_VEHICLE_POPUP_ON_START];
                }else
                {
                    return true;
                }

                return _showSampleVehiclePopupOnStart;
            }

            set
            {
                if (__isolatedStorageSettings.Contains(SHOW_SAMPLE_VEHICLE_POPUP_ON_START))
                     __isolatedStorageSettings[SHOW_SAMPLE_VEHICLE_POPUP_ON_START] = value;
                else
                    __isolatedStorageSettings.Add(SHOW_SAMPLE_VEHICLE_POPUP_ON_START, value);
            }
        }

    }
}
