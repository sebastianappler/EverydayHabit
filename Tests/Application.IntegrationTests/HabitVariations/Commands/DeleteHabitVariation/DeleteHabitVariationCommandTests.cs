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
    public class DeleteHabitVariationCommandTests : CommandTestBase
    {
        [Fact]
        public async Task Handle_GivenValidIdAndZeroOrders_DeletesCustomer()
        {
            var validId = 1;

            var command = new DeleteHabitVariationCommand { Id = validId };

            var mediatorMock = new Mock<IMediator>();
            var sut = new DeleteHabitVariationCommand.Handler(_context, mediatorMock.Object);
            await sut.Handle(command, CancellationToken.None);

            var habitVariation = await _context.HabitVariations.FindAsync(validId);

            Assert.Null(habitVariation);
        }
    }
}
