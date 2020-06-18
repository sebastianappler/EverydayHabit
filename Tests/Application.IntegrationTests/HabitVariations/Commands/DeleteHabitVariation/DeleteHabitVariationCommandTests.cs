using Application.IntegrationTests.Common;
using EverydayHabit.Application.HabitVariations.Commands.DeleteHabitVariation;
using MediatR;
using Moq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitVariations.Commands.CreateHabitVariation
{
    public class DeleteHabitVariationCommandTests : CommandTestBase
    {
        [Fact]
        public async Task ShouldDeleteHabitVariation()
        {
            var habitVarioationId = 1;
            var command = new DeleteHabitVariationCommand { Id = habitVarioationId };

            var mediatorMock = new Mock<IMediator>();
            var sut = new DeleteHabitVariationCommand.Handler(_context, mediatorMock.Object);
            await sut.Handle(command, CancellationToken.None);

            var habitVariation = await _context.HabitVariations.FindAsync(habitVarioationId);

            Assert.Null(habitVariation);
        }
    }
}
