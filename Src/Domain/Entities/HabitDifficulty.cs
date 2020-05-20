using EverydayHabit.Domain.Enums;

namespace EverydayHabit.Domain.Entities
{
    public class HabitDifficulty
    {
        public int HabitDifficultyId { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }
        public string Description { get; set; }

        public HabitVariation HabitVariant { get; set; }
    }
}