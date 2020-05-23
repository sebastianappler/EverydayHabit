using Application.Habits.Commands.CreateHabit;
using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitCompletions.Commands.CreateHabitCompletion;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.CreateHabit
{
    public class CreateHabitVariationCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldCreateHabitVariation()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateHabitVariationCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitVariationId = await sut.Handle(new CreateHabitVariationCommand
            {
                HabitId = 1,
                Name = "Running",
            }, CancellationToken.None);

            // Assert
            var habit = await _context.Habits.FindAsync(1);
            habit.Variants.First().HabitVariantName.ShouldBe("Running");
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitVariationCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateHabitVariationCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitVariationId = await sut.Handle(new CreateHabitVariationCommand
            { 
                HabitId = 1,
                Name = "Running",
            }, CancellationToken.None);
            
            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitVariationCreated>(h => h.HabitVariationId == habitVariationId), It.IsAny<CancellationToken>()), Times.Once);
        }
       
    }
}
