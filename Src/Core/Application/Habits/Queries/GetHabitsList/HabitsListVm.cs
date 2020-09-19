using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class HabitsListVm
    {
        public IList<HabitListDto> Habits { get; set; }
    }
}
