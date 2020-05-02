using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitsListQuery;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.Habits.Queries
{
    [Collection("QueryCollection")]
    public class GetHabitsListQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitsListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitsTest()
        {
            var sut = new GetHabitsListQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetHabitsListQuery(), CancellationToken.None);

            result.ShouldBeOfType<HabitsListVm>();

            result.Habits.Count.ShouldBe(3);
        }
    }
}
