using AutoMapper;
using EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
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

            var result = _mapper.Map<HabitVariationDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitVariationDto>();
        }

        [Fact]
        public void ShouldMapHabitDifficultyToHabitDifficultyDto()
        {
            var entity = new HabitDifficulty();

            var result = _mapper.Map<HabitDifficultyDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitDifficultyDto>();
        }

        [Fact]
        public void ShouldMapHabitToHabitCompletionHabitDto()
        {
            var entity = new Habit();

            var result = _mapper.Map<HabitCompletionHabitDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitCompletionHabitDto>();
        }

        [Fact]
        public void ShouldMapHabitVariationToHabitCompletionVariationDto()
        {
            var entity = new HabitVariation();

            var result = _mapper.Map<HabitCompletionVariationDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitCompletionVariationDto>();
        }

        [Fact]
        public void ShouldMapHabitDifficultyToHabitCompletionDifficultyDto()
        {
            var entity = new HabitDifficulty();

            var result = _mapper.Map<HabitCompletionDifficultyDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitCompletionDifficultyDto>();
        }

        [Fact]
        public void ShouldMapHabitCompletionToHabitCompletionsListDto()
        {
            var entity = new HabitCompletion();

            var result = _mapper.Map<HabitCompletionsListDto>(entity);

            result.ShouldNotBeNull();
            result.ShouldBeOfType<HabitCompletionsListDto>();
        }
    }
}
