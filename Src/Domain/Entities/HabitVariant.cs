namespace EverydayHabit.Domain.Entities
{
    public class HabitVariant
    {
        public int HabitVariantId { get; set; }
        public HabitDifficulty Mini { get; set; }
        public HabitDifficulty Plus { get; set; }
        public HabitDifficulty Elite { get; set; }
    }
}
