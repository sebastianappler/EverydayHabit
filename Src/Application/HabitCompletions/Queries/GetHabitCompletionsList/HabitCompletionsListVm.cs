﻿using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class HabitCompletionsListVm
    {
        public IList<HabitCompletionsListDto> Habits { get; set; }
    }
}