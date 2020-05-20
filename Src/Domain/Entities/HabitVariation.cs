using System.Collections.Generic;

namespace EverydayHabit.Domain.Entities
{
    public class HabitVariation
    {
        public HabitVariation()
        {
            HabitDifficulties = new HashSet<HabitDifficulty>();
        }

        public int HabitVariantId { get; set; }
        public int HabitId { get; set; }
        public string HabitVariantName { get; set; }

        public Habit Habit { get; set; }
        public ICollection<HabitDifficulty> HabitDifficulties { get; set; }
    }
}
