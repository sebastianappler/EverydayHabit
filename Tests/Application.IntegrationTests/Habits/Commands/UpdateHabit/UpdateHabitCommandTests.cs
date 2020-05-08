using Application.Habits.Commands.CreateHabit;
using Application.IntegrationTests.Common;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.CreateHabit
{
    public class UpdateHabitCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitUpdatedNotification()
        {
            //// Arrange
            //var mediatorMock = new Mock<IMediator>();
            //var sut = new CreateHabitCommand.Handler(_context, mediatorMock.Object);
            //var habitName = "Test Habit";

            //// Act
            //var habitId = await sut.Handle(new CreateHabitCommand { Name = habitName }, CancellationToken.None);


            //// Assert
            //mediatorMock.Verify(m => m.Publish(It.Is<HabitUpdated>(h => h.HabitId == habitId), It.IsAny<CancellationToken>()), Times.Once);

        }
    }
}
