using EverydayHabit.Domain.Enums;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public static class HabitTypeToIconConverter
    {
        public static string Convert(HabitType habitType)
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
                default:
                    return "bubble_chart";
            }
        }
    }
}
