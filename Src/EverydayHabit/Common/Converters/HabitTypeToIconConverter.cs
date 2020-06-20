using EverydayHabit.Domain.Enums;

namespace EverydayHabit.XamarinApp.Common.Converters
{
    public static class HabitTypeToIconConverter
    {
        public static string Convert(HabitType habitType)
        {
            switch (habitType)
            {
                case HabitType.Training:
                    return "fitness_center";
                default:
                    return "bubble_chart";
            }
        }
    }
}
