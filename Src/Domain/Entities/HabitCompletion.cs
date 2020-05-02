using EverydayHabit.Domain.Enums;
using System;

namespace EverydayHabit.Domain.Entities
{
    public class HabitCompletion
    {
        public int HabitCompletionId { get; set; }
        public DateTime Date { get; set; }
        public Habit CompletedHabit { get; set; }
        public HabitDifficultyLevel HabitDifficultyLevel { get; set; }
    }
}
