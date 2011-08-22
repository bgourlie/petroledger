using System;
using System.Globalization;
using System.Windows.Data;

namespace Petroledger.Converters
{
    public class PriceToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var price = (double) value;
            return price == 0 ? String.Empty : price.ToString("C3");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return double.Parse((string) value, NumberStyles.Currency);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        #endregion
    }
}