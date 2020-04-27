using ElasticHabitCalendar.Application.Common.Interfaces;
using ElasticHabitCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElasticHabitCalendar.Persistance
{
    public class ElasticHabitCalendarDbContext : IElasticHabitCalendarDbContext
    {
        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }
    }
}
