﻿using System;
using System.Globalization;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Converters
{
	public class ItemTappedEventArgsToItemTappedConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var eventArgs = value as ItemTappedEventArgs;
			return eventArgs.Item;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
