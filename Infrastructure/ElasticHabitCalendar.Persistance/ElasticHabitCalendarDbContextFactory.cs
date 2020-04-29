using Microsoft.EntityFrameworkCore;

namespace ElasticHabitCalendar.Persistance
{
    public class ElasticHabitCalendarDbContextFactory : DesignTimeDbContextFactoryBase<ElasticHabitCalendarDbContext>
    {
        protected override ElasticHabitCalendarDbContext CreateNewInstance(DbContextOptions<ElasticHabitCalendarDbContext> options)
        {
            return new ElasticHabitCalendarDbContext(options);
        }
    }
}
