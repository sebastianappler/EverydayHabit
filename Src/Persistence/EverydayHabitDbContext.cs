using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Persistence
{
    public class EverydayHabitDbContext : DbContext, IEverydayHabitDbContext
    {
        public EverydayHabitDbContext(DbContextOptions<EverydayHabitDbContext> options) : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitVariation> HabitVariations { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EverydayHabitDbContext).Assembly);
        }
    }
}
