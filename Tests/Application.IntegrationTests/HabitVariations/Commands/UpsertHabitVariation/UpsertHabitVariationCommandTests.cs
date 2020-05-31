using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitVariations.Commands.CreateHabitVariation
{
    public class UpsertHabitVariationCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateHabitVariation()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitVariationCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            await sut.Handle(new UpsertHabitVariationCommand
            {
                HabitId = 1,
                Name = "Running",
            }, CancellationToken.None);

            // Assert
            var habit = await _context.Habits.FindAsync(1);
            habit.Variants.First().HabitVariantName.ShouldBe("Running");
        }

        [Fact]
        public async Task ShouldUpdateHabitVariation()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitVariationCommand.Handler(_context, mediatorMock.Object);

            // Act
            await sut.Handle(new UpsertHabitVariationCommand
            {
                Id = 1,
                HabitId = 1,
                Name = "Sprinting",
            }, CancellationToken.None);

            // Assert
            var habit = await _context.Habits.FindAsync(1);
            habit.Variants.First().HabitVariationId.ShouldBe(1);
            habit.Variants.First().HabitVariantName.ShouldBe("Sprinting");
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitVariationCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitVariationCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitVariationId = await sut.Handle(new UpsertHabitVariationCommand
            {
                HabitId = 1,
                Name = "Running",
            }, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitVariationCreated>(h => h.HabitVariationId == habitVariationId), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
