using Microsoft.EntityFrameworkCore;

namespace ElasticHabitCalendar.Persistence
{
    public class ElasticHabitCalendarDbContextFactory : DesignTimeDbContextFactoryBase<ElasticHabitCalendarDbContext>
    {
        protected override ElasticHabitCalendarDbContext CreateNewInstance(DbContextOptions<ElasticHabitCalendarDbContext> options)
        {
            return new ElasticHabitCalendarDbContext(options);
        }
    }
}
