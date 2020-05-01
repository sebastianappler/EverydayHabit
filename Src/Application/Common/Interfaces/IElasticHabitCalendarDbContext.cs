using ElasticHabitCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticHabitCalendar.Androidlication.Common.Interfaces
{
    public interface IElasticHabitCalendarDbContext
    {
        DbSet<Habit> Habits { get; set; }
        DbSet<HabitCompletion> HabitCompletions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
