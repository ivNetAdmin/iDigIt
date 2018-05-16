using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace iDigIt.Converters
{
    public class NativeTypeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            // No format provided.
            if (parameter == null)
                return value;

            string[] parameters = parameter.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string format = (parameters.Length > 1 ? parameters[1] : "");
            return this.FormatString(value, format);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string[] parameters = parameter.ToString().Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            string type = parameters[0];

            if (type == "Decimal")
                return decimal.Parse(value.ToString());
            if (type == "Int")
                return int.Parse(value.ToString());
            if (type == "DateTime")
            {
                return new DateTimeOffset(new DateTime(((DateTime)value).Year, ((DateTime)value).Month, ((DateTime)value).Day));
            }
            if (type == "DateTimeOffset")
            {
                if (value.GetType() == typeof(DateTime))
                {
                    return new DateTimeOffset(new DateTime(((DateTime)value).Year, ((DateTime)value).Month, ((DateTime)value).Day));
                }
                return ((DateTimeOffset)value).LocalDateTime;
            }
            return value;
        }

        private string FormatString(object value, string format)
        {
            if (value != null && string.IsNullOrEmpty(format))
                return value.ToString();

            if (format == "DateTimeOffsetString")
                return (((DateTimeOffset)value).LocalDateTime).ToString("dd-MMM-yy");

            //return DateTimeOffset.Parse(value.ToString()).ToString("dd-MMM-yy");

            if(format == "DateTimeOffsetFrostString")
                return (((DateTimeOffset)value).LocalDateTime).ToString("dd-MMM");

            if (format == "JobTime")
               return GetTime((int)value);                

            //return DateTimeOffset.Parse(value.ToString()).ToString("dd-MMM");

            var plantName = (string)value;

            if (!string.IsNullOrEmpty(plantName))
            {
                if (format == "PlantName")
                {

                    if (plantName.IndexOf("*") != -1)
                    {
                        return plantName.Substring(0, plantName.IndexOf("*") - 1).Trim();
                    }
                }

                if (format == "PlantVariety")
                {
                    if (plantName.IndexOf("*") != -1)
                    {
                        return plantName.Substring(plantName.IndexOf("*") + 1).Trim();
                    }
                }
            }
            return System.Convert.ToString(value);
        }

        private string GetTime(int value)
        {
            if(value>120)
            {
                var hours = value/ 60;
                return string.Format("{0}hours {1}mins", hours,(double)value-(hours*60));
            }
            return string.Format("{0}mins", value);
        }
    }
}