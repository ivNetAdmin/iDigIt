using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace iDigIt.Converters
{
    public class ZeroBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var s = System.Convert.ToInt32(value);
            return s != 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
