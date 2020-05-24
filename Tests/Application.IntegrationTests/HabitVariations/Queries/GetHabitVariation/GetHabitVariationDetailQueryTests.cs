using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitVariations.Queries.GetHabitVariation
{
    [Collection("QueryCollection")]
    public class GetHabitVariationDetailQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitVariationDetailQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetHabitDetailTest()
        {
            var sut = new GetHabitVariationDetailQuery.Handler(_context, _mapper);

            var result = await sut.Handle(new GetHabitVariationDetailQuery { Id = 1 }, CancellationToken.None);

            result.ShouldBeOfType<HabitVariationDetailVm>();
            result.HabitId.ShouldBe(1);
            result.Name.ShouldBe("Running");
        }
    }
}
