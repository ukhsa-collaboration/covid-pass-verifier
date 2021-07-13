using System;
using System.Globalization;
using NHSCovidPassVerifier.Utils;
using Xamarin.Forms;

namespace NHSCovidPassVerifier.Controls.Converters
{
    public class DateTimeToResultFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var returnString = "Invalid DateTime";
            
            if (value is DateTime input)
                returnString = input.FormatDateTime();

            return returnString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DateTime.Parse((string) value);
        }
    }
}