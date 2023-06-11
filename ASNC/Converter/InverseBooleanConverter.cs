using System;
using System.Globalization;
using System.Windows.Data;

namespace ASNC.Converter
{
    internal class InverseBooleanConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                return !b;
            }

            throw new ArgumentException($"{nameof(value)} is not a boolean");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => this.Convert(value, targetType, parameter, culture);
    }
}
