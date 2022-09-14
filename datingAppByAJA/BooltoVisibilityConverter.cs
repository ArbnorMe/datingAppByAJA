using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using System.Linq;
using System.Windows.Data;
using System.Windows;

namespace datingAppByAJA
{
    public class BooltoVisibilityConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetype, object parameter, CultureInfo culture)
        {
            bool hasText = !(bool)values[0];
            bool hasFocus = (bool)values[1];

            if (hasText || hasFocus)
                return Visibility.Collapsed;
            return Visibility.Visible;
        }
        public object PwConvert(object[] values, Type targetype, object parameter, CultureInfo culture)
        {
            bool hasPassword = !(bool)values[0];
            bool hasFocus = (bool)values[1];

            if (hasPassword || hasFocus)
                return Visibility.Collapsed;
                return Visibility.Visible;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
