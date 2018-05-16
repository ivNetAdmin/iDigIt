using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using iDigIt.Models;
using Xamarin.Forms;

namespace iDigIt.Converters
{
    public class JobTypeHighlightColorConverter : IValueConverter
    {
        const string CULTIVATE_COLOR = "#c8e9ca";
        const string PREPARATION_COLOR = "#e2d4cf";
        const string GENERAL_COLOR = "#ffe6cc";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || parameter == null) return Color.White;

            Color rowcolor = Color.FromHex(GENERAL_COLOR);

            var jobType = ((Job)value).Type;
            switch (jobType)
            {
                case "Cultivate":
                    rowcolor = rowcolor = Color.FromHex(CULTIVATE_COLOR);
                    break;
                case "Preparation":
                    rowcolor = rowcolor = Color.FromHex(PREPARATION_COLOR);
                    break;
                case "General":
                    rowcolor = rowcolor = Color.FromHex(GENERAL_COLOR);
                    break;

            }

            return rowcolor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
