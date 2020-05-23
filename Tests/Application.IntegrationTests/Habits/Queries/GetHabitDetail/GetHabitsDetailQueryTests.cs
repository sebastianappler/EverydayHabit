using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Queries.GetHabitsList
{
    [Collection("QueryCollection")]
    public class GetHabitsDetailQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitsDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitDetailTest()
        {
            var sut = new GetHabitDetailQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetHabitDetailQuery { Id = 1}, CancellationToken.None);

            result.ShouldBeOfType<HabitDetailVm>();
            result.Id.ShouldBe(1);
            result.Name.ShouldBe("Training");
            result.VariationsList.ShouldNotBeEmpty();
        }
    }
}
