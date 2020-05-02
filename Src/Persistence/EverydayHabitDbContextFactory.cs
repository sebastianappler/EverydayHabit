using Microsoft.EntityFrameworkCore;

namespace EverydayHabit.Persistence
{
    public class EverydayHabitDbContextFactory : DesignTimeDbContextFactoryBase<EverydayHabitDbContext>
    {
        protected override EverydayHabitDbContext CreateNewInstance(DbContextOptions<EverydayHabitDbContext> options)
        {
            return new EverydayHabitDbContext(options);
        }
    }
}
