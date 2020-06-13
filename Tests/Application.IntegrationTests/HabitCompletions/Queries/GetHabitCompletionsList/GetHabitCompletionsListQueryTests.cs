using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Queries.GetHabitsList
{
    [Collection("QueryCollection")]
    public class GetHabitCompletionsListQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitCompletionsListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitsTest()
        {
            var sut = new GetHabitCompletionsListQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetHabitCompletionsListQuery { HabitId = 1 }, CancellationToken.None);

            result.ShouldBeOfType<HabitCompletionsListVm>();

            result.HabitCompletionsList.Count.ShouldBe(2);
        }
    }
}
