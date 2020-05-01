using System.Collections.Generic;

namespace ElasticHabitCalendar.Domain.Entities
{
    public class Habit
    {
        public Habit()
        {
            Variants = new HashSet<HabitVariant>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HabitVariant> Variants { get; set; }
    }
}
