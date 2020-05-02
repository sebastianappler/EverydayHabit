using EverydayHabit.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace Application.IntegrationTests.Common
{
    public class EverydayHabitContextFactory
    {
        public static EverydayHabitDbContext Create()
        {
            var options = new DbContextOptionsBuilder<EverydayHabitDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new EverydayHabitDbContext(options);
            context.Database.EnsureCreated();

            return context;
        }
        public static void Destroy(EverydayHabitDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
