using EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos;
using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class HabitCompletionsListVm
    {
        public IList<HabitCompletionsListDto> HabitCompletionsList { get; set; }
    }
}
