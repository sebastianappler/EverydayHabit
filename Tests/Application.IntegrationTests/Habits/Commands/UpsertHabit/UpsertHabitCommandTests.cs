using Application.Habits.Commands.CreateHabit;
using Application.IntegrationTests.Common;
using EverydayHabit.Application.Habits.Commands.UpsertHabit;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.UpsertHabit
{
    public class UpsertHabitCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitCommand.Handler(_context, mediatorMock.Object);
            var habitName = "Test Habit";

            // Act
            var habitId = await sut.Handle(new UpsertHabitCommand { Name = habitName }, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitCreated>(h => h.HabitId == habitId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitUpdatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitCommand.Handler(_context, mediatorMock.Object);
            var habitName = "Test Habit";

            // Act
            var habitId = await sut.Handle(new UpsertHabitCommand { Id = 1, Name = habitName }, CancellationToken.None);

            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitUpdated>(h => h.HabitId == habitId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShouldUpdatedHabit()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sutCreate = new UpsertHabitCommand.Handler(_context, mediatorMock.Object);
            var sutUpdate = new UpsertHabitCommand.Handler(_context, mediatorMock.Object);

            // Act
            var habitId = await sutCreate.Handle(new UpsertHabitCommand
            {
                Name = "New Habit",
                Description = "New Habit Description",
                HabitType = HabitType.Training
            }, CancellationToken.None);

            await sutUpdate.Handle(new UpsertHabitCommand
            {
                Id = habitId,
                Name = "Updated habit",
                Description = "Updated habit Description",
                HabitType = HabitType.Music
            }, CancellationToken.None);

            var upadetHabit = await _context.Habits.FindAsync(habitId);

            // Assert
            upadetHabit.Name.ShouldBe("Updated habit");
            upadetHabit.Description.ShouldBe("Updated habit Description");
            upadetHabit.HabitType.ShouldBe(HabitType.Music);
        }
    }
}
