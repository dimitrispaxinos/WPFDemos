using System;
using System.Windows;
using System.Windows.Data;

namespace WatermarkSample.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var val = (bool)value;

            var param = (string)parameter;
            if (!String.IsNullOrEmpty(param) && param.Equals("Inverted"))
                return !val ? System.Windows.Visibility.Visible : Visibility.Collapsed;

            return val ? System.Windows.Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
