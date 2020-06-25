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
                new Habit { HabitId = 1, Name = "Training", HabitType = HabitType.Training },
                new Habit { HabitId = 2, Name = "Develop EverydayHabits App", HabitType = HabitType.Project  },
                new Habit { HabitId = 3, Name = "Learn German", HabitType = HabitType.Language },
            });

            var habit = context.Habits.Find(1);

            habit.Variants.Add(new HabitVariation
            {
                //Id = 1
                HabitVariantName = "Running",
                HabitId = 1,
                Habit = habit
            });
            
            habit.Variants.Add(new HabitVariation
            {
                //Id = 2
                HabitVariantName = "Pushups",
                HabitId = 1,
                Habit = habit
            });
            
            habit.Variants.Add(new HabitVariation
            {
                //Id = 3
                HabitVariantName = "Language",
                HabitId = 2,
                Habit = habit
            });

            context.HabitDifficulties.AddRange(new[] {
                new HabitDifficulty
                {
                    //Id = 1
                    DifficultyLevel = HabitDifficultyLevel.Mini,
                    Description = "Run for 100 meters",
                    HabitVariationId = 1,
                },
                new HabitDifficulty
                {
                    //Id = 2
                    DifficultyLevel = HabitDifficultyLevel.Plus,
                    Description = "Run for 500 meters",
                    HabitVariationId = 1
                },
                new HabitDifficulty
                {
                    //Id = 3
                    DifficultyLevel = HabitDifficultyLevel.Elite,
                    Description = "Run for 1500 meters",
                    HabitVariationId = 1
                },
                new HabitDifficulty
                {
                    //Id = 4
                    DifficultyLevel = HabitDifficultyLevel.Plus,
                    Description = "Do 10 pushups",
                    HabitVariationId = 2
                },
                new HabitDifficulty
                {
                    //Id = 5
                    DifficultyLevel = HabitDifficultyLevel.Plus,
                    Description = "Learn 10 words 10 pushups",
                    HabitVariationId = 3
                },
            });
            context.HabitCompletions.AddRange(new[] {
                new HabitCompletion { 
                    HabitCompletionId = 1, 
                    Date = DateTime.Now, 
                    HabitDifficultyId = 1, 
                    HabitId = 1
                },
                new HabitCompletion { 
                    HabitCompletionId = 2, 
                    Date = DateTime.Now.AddDays(-1), 
                    HabitDifficultyId = 2, 
                    HabitId = 1},
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
