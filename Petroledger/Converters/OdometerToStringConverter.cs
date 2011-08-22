using System;
using System.Globalization;
using System.Windows.Data;

namespace Petroledger.Converters
{
    public class OdometerToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return string.Empty;

            var odometerReading = (double) value;
            return odometerReading == 0 ? String.Empty : odometerReading.ToString("###,###.#");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return double.Parse((string) value);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        #endregion
    }
}