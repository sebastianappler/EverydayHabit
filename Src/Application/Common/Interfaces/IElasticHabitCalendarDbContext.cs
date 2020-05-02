using EverydayHabit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Androidlication.Common.Interfaces
{
    public interface IEverydayHabitDbContext
    {
        DbSet<Habit> Habits { get; set; }
        DbSet<HabitCompletion> HabitCompletions { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
