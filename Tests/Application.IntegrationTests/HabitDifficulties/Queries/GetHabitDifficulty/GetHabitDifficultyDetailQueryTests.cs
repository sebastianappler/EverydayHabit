using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Domain.Enums;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitVariations.Queries.GetHabitVariation
{
    [Collection("QueryCollection")]
    public class GetHabitDifficultyDetailQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitDifficultyDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitDetailTest()
        {
            var sut = new GetHabitDifficultyDetailQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetHabitDifficultyDetailQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<HabitDifficultyDetailVm>();
            result.Id.ShouldBe(1);
            result.Description.ShouldBe("Run for 100 meters");
            result.DifficultyLevel.ShouldBe(HabitDifficultyLevel.Mini);
        }
    }
}
