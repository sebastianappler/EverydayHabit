using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion;
using EverydayHabit.Domain.Enums;
using MediatR;
using Moq;
using Shouldly;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Commands.CreateHabit
{
    public class UpsertHabitCompletionCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitCompletionCreatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitCompletionCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitCompletionId = await sut.Handle(new UpsertHabitCompletionCommand { 
                HabitId = 1,
                HabitVariationId = 1,
                Date = dateTime,
                HabitDifficultyId = 2
            }, CancellationToken.None);
            
            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitCompletionCreated>(h => h.HabitCompletionId == habitCompletionId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_GivenValidRequest_ShouldRaiseHabitCompletionUpdatedNotification()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sut = new UpsertHabitCompletionCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitCompletionId = await sut.Handle(new UpsertHabitCompletionCommand { 
                Id = 1,
                HabitId = 1,
                HabitVariationId = 1,
                Date = dateTime,
                HabitDifficultyId = 1,
            }, CancellationToken.None);
            
            // Assert
            mediatorMock.Verify(m => m.Publish(It.Is<HabitCompletionUpdated>(h => h.HabitCompletionId == habitCompletionId), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ShouldUpsertHabitCompletion()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();
            var sutCreate = new UpsertHabitCompletionCommand.Handler(_context, mediatorMock.Object);
            var sutUpdate = new UpsertHabitCompletionCommand.Handler(_context, mediatorMock.Object);
            var dateTime = DateTime.Now;

            // Act
            var habitCompletionId = await sutCreate.Handle(new UpsertHabitCompletionCommand
            {
                HabitId = 1,
                HabitVariationId = 1,
                Date = dateTime,
                HabitDifficultyId = 2,
            }, CancellationToken.None);

            await sutUpdate.Handle(new UpsertHabitCompletionCommand
            {
                Id = habitCompletionId,
                HabitId = 1,
                HabitVariationId = 2,
                Date = dateTime.AddDays(1),
                HabitDifficultyId = 4,
            }, CancellationToken.None);

            var habitCompletion = await _context.HabitCompletions.FindAsync(habitCompletionId);

            // Assert
            habitCompletion.HabitId.ShouldBe(1);
            habitCompletion.HabitVariationId.ShouldBe(2);
            habitCompletion.Date.ShouldBe(dateTime.AddDays(1));
            habitCompletion.HabitDifficultyId.ShouldBe(4);
        }
    }
}
