using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
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

        [Fact]
        public void ShouldMapHabitToHabitDetailVm()
        {
            var entity = new Habit();

            var result = _mapper.Map<HabitDetailVm>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitDetailVm>();
        }

        [Fact]
        public void ShouldMapHabitVariantToHabitVariantDto()
        {
            var entity = new HabitVariation();

            var result = _mapper.Map<HabitVariantDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitVariantDto>();
        }

        [Fact]
        public void ShouldMapHabitDifficultyToHabitDifficultyDto()
        {
            var entity = new HabitDifficulty();

            var result = _mapper.Map<HabitDifficultyDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitDifficultyDto>();
        }
    }
}
