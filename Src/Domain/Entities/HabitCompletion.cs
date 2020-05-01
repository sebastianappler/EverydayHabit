using ElasticHabitCalendar.Domain.Enums;
using System;

namespace ElasticHabitCalendar.Domain.Entities
{
    public class HabitCompletion
    {
        public int HabitCompletionId { get; set; }
        public DateTime Date { get; set; }
        public Habit CompletedHabit { get; set; }
        public HabitDifficultyLevel HabitDifficultyLevel { get; set; }
    }
}
