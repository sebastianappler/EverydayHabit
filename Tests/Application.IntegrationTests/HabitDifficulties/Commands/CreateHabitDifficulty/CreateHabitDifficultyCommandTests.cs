using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitDifficulties.Commands.CreateHabitDifficulty;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using Shouldly;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitDifficulties.Commands.CreateHabitDifficulty
{
    public class CreateHabitDifficultyCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateHabitDifficulty()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sut.Handle(new CreateHabitDifficultyCommand
            {
                HabitVariationId = 1,
                Description = "Running for 2 minutes",
                DifficultyLevel = HabitDifficultyLevel.Mini,
            }, CancellationToken.None);

            // Assert
            var habitVariation = await _context.HabitVariations.FindAsync(1);
            habitVariation.HabitDifficulties.First().HabitDifficultyId.ShouldBe(habitDifficultyId);
            habitVariation.HabitDifficulties.First().DifficultyLevel.ShouldBe(HabitDifficultyLevel.Mini);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitDifficultyCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sut.Handle(new CreateHabitDifficultyCommand
            {
                HabitVariationId = 1,
                Description = "Running for 2 minutes",
                DifficultyLevel = HabitDifficultyLevel.Mini,
            }, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitDifficultyCreated>(h => h.HabitDifficultyId == habitDifficultyId), It.IsAny<CancellationToken>()), Times.Once);
        }

    }
}
