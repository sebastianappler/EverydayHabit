using Application.Habits.Commands.CreateHabit;
using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitDifficulties.Commands.CreateHabitDifficulty;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.CreateHabit
{
    public class UpdateHabitDifficultyCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldUpdatedHabitDifficulty()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sutCreate = new CreateHabitDifficultyCommand.Handler(_context, mediatorMock.Object);
            var sutUpdate = new UpdateHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sutCreate.Handle(new CreateHabitDifficultyCommand
            {
                HabitVariationId = 1,
                Description = "New difficulty",
                DifficultyLevel = HabitDifficultyLevel.Mini
            }, CancellationToken.None);

            await sutUpdate.Handle(new UpdateHabitDifficultyCommand
            {
                Id = habitDifficultyId,
                Description = "Updated difficulty",
                DifficultyLevel = HabitDifficultyLevel.Elite
            }, CancellationToken.None);

            var habitDifficulty = await _context.HabitDifficulties.FindAsync(habitDifficultyId);

            // Assert
            habitDifficulty.HabitDifficultyId.ShouldBe(habitDifficultyId);
            habitDifficulty.HabitVariationId.ShouldBe(1);
            habitDifficulty.Description.ShouldBe("Updated difficulty");
            habitDifficulty.DifficultyLevel.ShouldBe(HabitDifficultyLevel.Elite);

        }
    }
}
