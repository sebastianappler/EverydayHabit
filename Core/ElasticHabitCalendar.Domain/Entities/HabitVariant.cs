namespace ElasticHabitCalendar.Domain.Entities
{
    public class HabitVariant
    {
        public int Id { get; set; }
        public HabitDifficulty Mini { get; set; }
        public HabitDifficulty Plus { get; set; }
        public HabitDifficulty Elite { get; set; }
    }
}
