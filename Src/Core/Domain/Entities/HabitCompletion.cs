﻿using System;

namespace EverydayHabit.Domain.Entities
{
    public class HabitCompletion
    {
        public int HabitCompletionId { get; set; }
        public int HabitId { get; set; }
        public int HabitVariationId { get; set; }
        public int HabitDifficultyId { get; set; }
        public DateTime Date { get; set; }

        public Habit Habit { get; set; }
    }
}
