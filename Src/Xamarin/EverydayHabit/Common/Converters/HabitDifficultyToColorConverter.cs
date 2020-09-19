using EverydayHabit.Domain.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public class HabitDifficultyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var difficulty = (HabitDifficultyLevel) value;
            var difficultyColor = ConvertToColor(difficulty);
            return difficultyColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value;
        }

        public static Color? ConvertToColor(HabitDifficultyLevel habitDifficultyLevel)
        {
            switch (habitDifficultyLevel)
            {
                case HabitDifficultyLevel.Mini:
                    App.Current.Resources.TryGetValue("PrimaryGreen", out var green);
                    return (Color)green;

                case HabitDifficultyLevel.Plus:
                    App.Current.Resources.TryGetValue("PrimaryYellow", out var yellow);
                    return (Color)yellow;

                case HabitDifficultyLevel.Elite:
                    App.Current.Resources.TryGetValue("PrimaryRed", out var red);
                    return (Color)red;
            }

            return null;
        }
    }
}
