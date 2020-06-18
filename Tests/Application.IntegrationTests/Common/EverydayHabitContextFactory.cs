using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
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

            context.Habits.AddRange(new[] {
                new Habit { HabitId = 1, Name = "Training" },
                new Habit { HabitId = 2, Name = "Develop EverydayHabits App" },
                new Habit { HabitId = 3, Name = "Learn German" },
            });

            var habit = context.Habits.Find(1);

            habit.Variants.Add(new HabitVariation
            {
                HabitVariantName = "Running",
                HabitId = 1,
                Habit = habit
            });

            context.HabitCompletions.AddRange(new[] {
                new HabitCompletion { 
                    HabitCompletionId = 1, 
                    Date = DateTime.Now, 
                    HabitDifficultyLevel = HabitDifficultyLevel.Plus, 
                    Habit = habit,
                    HabitId = habit.HabitId
                },
                new HabitCompletion { 
                    HabitCompletionId = 2, 
                    Date = DateTime.Now.AddDays(-1), 
                    HabitDifficultyLevel = HabitDifficultyLevel.Mini, 
                    Habit = habit,
                    HabitId = habit.HabitId},
            });

            context.SaveChanges();

            return context;
        }
        public static void Destroy(EverydayHabitDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}
