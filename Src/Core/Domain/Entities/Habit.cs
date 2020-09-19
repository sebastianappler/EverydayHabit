using EverydayHabit.Domain.Enums;
using System.Collections.Generic;

namespace EverydayHabit.Domain.Entities
{
    public class Habit
    {
        public Habit()
        {
            Variants = new HashSet<HabitVariation>();
        }

        public int HabitId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HabitType HabitType { get; set; }
        public ICollection<HabitVariation> Variants { get; set; }
    }
}
