using System;
using System.Globalization;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Converters
{
	public class EventArgsToEventArgsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var eventArgs = value as EventArgs;
			return eventArgs;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
