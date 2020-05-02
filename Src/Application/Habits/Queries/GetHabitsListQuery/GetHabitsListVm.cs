using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsListQuery
{
    public class GetHabitsListVm
    {
        public IList<GetHabitsListDto> Habits { get; set; }
    }
}
