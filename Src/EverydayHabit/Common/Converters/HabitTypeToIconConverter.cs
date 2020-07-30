using EverydayHabit.Domain.Enums;
using System;
using System.Globalization;
using Xamarin.Forms;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public class HabitTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var habitType = (HabitType) value;
            var habitTypeIcon = ConvertToIcon(habitType);
            return habitTypeIcon;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value;
        }
        public static string ConvertToIcon(HabitType habitType)
        {
            switch (habitType)
            {
                case HabitType.None:
                    return "bubble_chart";
                case HabitType.Music:
                    return "music_note";
                case HabitType.Training:
                    return "fitness_center";
                case HabitType.Reading:
                    return "menu_book";
                case HabitType.Meditation:
                    return "self_improvement";
                case HabitType.Language:
                    return "translate";
                case HabitType.Environmental:
                    return "eco";
                case HabitType.Project:
                    return "laptop";
                case HabitType.Art:
                    return "brush";
                case HabitType.Social:
                    return "emoji_people";
                case HabitType.Sports:
                    return "sports_tennis";
                case HabitType.MorningRoutine:
                    return "local_cafe";
                case HabitType.Planning:
                    return "today";
                default:
                    return "bubble_chart";
            }
        }
    }
}
