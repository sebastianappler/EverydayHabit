using ElasticHabitCalendar.AndroidApplication.Common.Interfaces;
using ElasticHabitCalendar.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ElasticHabitCalendar.Persistence
{
    public class ElasticHabitCalendarDbContext : DbContext, IElasticHabitCalendarDbContext
    {
        public ElasticHabitCalendarDbContext(DbContextOptions<ElasticHabitCalendarDbContext> options) : base(options)
        {
        }

        public DbSet<Habit> Habits { get; set; }
        public DbSet<HabitCompletion> HabitCompletions { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ElasticHabitCalendarDbContext).Assembly);
        }
    }
}
