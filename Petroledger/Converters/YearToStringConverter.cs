using System;
using System.Globalization;
using System.Windows.Data;

namespace Petroledger.Converters
{
    public class YearToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var intVal = (int) value;
            return intVal == 0 ? string.Empty : intVal.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var yearString = value as string;

            if (String.IsNullOrEmpty(yearString))
                return 0;

            try
            {
                return int.Parse((string) value);
            }
            catch (FormatException)
            {
                return -1;
            }
        }

        #endregion
    }
}