using System;
using System.Globalization;
using System.Windows.Data;

namespace Petroledger.Converters
{
    public class FillAmountToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var fillAmount = (double) value;
            return fillAmount.ToString("##.###");
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