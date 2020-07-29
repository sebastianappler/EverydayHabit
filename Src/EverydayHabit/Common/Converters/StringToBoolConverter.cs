using System;
using System.Globalization;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public class StringToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrEmpty((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string) value;
        }
    }
}
