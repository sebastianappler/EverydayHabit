using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitCompletions.Commands.CreateHabitCompletion;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.CreateHabit
{
    public class CreateHabitCompletionCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitCompletionCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new CreateHabitCompletionCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitCompletionId = await sut.Handle(new CreateHabitCompletionCommand { 
                HabitId = 1,
                Date = dateTime,
                HabitDifficultyLevel = HabitDifficultyLevel.Plus
            }, CancellationToken.None);
            
            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitCompletionCreated>(h => h.HabitCompletionId == habitCompletionId), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
