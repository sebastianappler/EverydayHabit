using EverydayHabit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Common.Interfaces
{
    public interface IEverydayHabitDbContext
    {
        DbSet<Habit> Habits { get; set; }
        DbSet<HabitVariation> HabitVariations { get; set; }
        DbSet<HabitCompletion> HabitCompletions { get; set; }
        DbSet<HabitDifficulty> HabitDifficulties { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
