using System.Collections.Generic;

namespace EverydayHabit.Domain.Entities
{
    public class Habit
    {
        public Habit()
        {
            Variants = new HashSet<HabitVariant>();
        }

        public int HabitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<HabitVariant> Variants { get; set; }
    }
}
