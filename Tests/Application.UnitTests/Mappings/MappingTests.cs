using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitsListQuery;
using EverydayHabit.Domain.Entities;
using Shouldly;
using Xunit;

namespace Application.UnitTests.Mappings
{
    public class MappingTests : IClassFixture<MappingTestsFixture>
    {
        private readonly IConfigurationProvider _configuration;
        private readonly IMapper _mapper;

        public MappingTests(MappingTestsFixture fixture)
        {
            _configuration = fixture.ConfigurationProvider;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public void ShouldHaveValidConfiguration()
        {
            _configuration.AssertConfigurationIsValid();
        }

        [Fact]
        public void ShouldMapHabitToHabitListDto()
        {
            var entity = new Habit();

            var result = _mapper.Map<HabitListDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitListDto>();
        }
    }
}
