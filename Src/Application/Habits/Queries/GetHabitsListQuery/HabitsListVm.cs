using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsListQuery
{
    public class HabitsListVm
    {
        public IList<HabitListDto> Habits { get; set; }
    }
}
