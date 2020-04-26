namespace ElasticHabitCalendar.Domain.Entities
{
    public class Habit
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HabitVariant Mini { get; set; }
        public HabitVariant Plus { get; set; }
        public HabitVariant Elite { get; set; }
    }
}
