using ElasticHabitCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElasticHabitCalendar.Application.Common.Interfaces
{
    public interface IElasticHabitCalendarDbContext
    {
        DbSet<Habit> Habits { get; set; }
        DbSet<HabitCompletion> HabitCompletions { get; set; }
    }
}
