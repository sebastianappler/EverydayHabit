using Application.Habits.Commands.CreateHabit;
using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitVariations.Commands.DeleteHabitVariation;
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
    public class DeleteHabitCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteHabit()
        {
            var habitId = 1;

            var command = new DeleteHabitCommand { Id = habitId };

            var mediatorMock = new Mock<IMediator>();
            var sut = new DeleteHabitCommand.Handler(_context, mediatorMock.Object);
            await sut.Handle(command, CancellationToken.None);

            var habit = await _context.Habits.FindAsync(habitId);

            Assert.Null(habit);
        }
    }
}
