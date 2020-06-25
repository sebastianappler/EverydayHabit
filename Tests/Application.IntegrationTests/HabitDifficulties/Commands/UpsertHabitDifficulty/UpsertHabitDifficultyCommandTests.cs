using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitDifficulties.Commands.UpsertHabitDifficulty;
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
    public class UpsertHabitDifficultyCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateHabitDifficulty()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sut.Handle(new UpsertHabitDifficultyCommand
            {
                HabitVariationId = 1,
                Description = "Running for 2 minutes",
                DifficultyLevel = HabitDifficultyLevel.Mini,
            }, CancellationToken.None);

            // Assert
            var habitVariation = await _context.HabitVariations.FindAsync(1);
            var habitDifficulty = habitVariation.HabitDifficulties.SingleOrDefault(hd => hd.HabitDifficultyId == habitDifficultyId);
            habitDifficulty.HabitDifficultyId.ShouldBe(habitDifficultyId);
            habitDifficulty.DifficultyLevel.ShouldBe(HabitDifficultyLevel.Mini);
        }

        [Fact]
        public async Task ShouldUpdatedHabitDifficulty()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sutCreate = new UpsertHabitDifficultyCommand.Handler(_context, mediatorMock.Object);
            var sutUpdate = new UpsertHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sutCreate.Handle(new UpsertHabitDifficultyCommand
            {
                HabitVariationId = 1,
                Description = "New difficulty",
                DifficultyLevel = HabitDifficultyLevel.Mini
            }, CancellationToken.None);

            await sutUpdate.Handle(new UpsertHabitDifficultyCommand
            {
                Id = habitDifficultyId,
                HabitVariationId = 1,
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

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitDifficultyCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitDifficultyCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitDifficultyId = await sut.Handle(new UpsertHabitDifficultyCommand
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
