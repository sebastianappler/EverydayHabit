using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos
{
    public class HabitVariationDto : IMapFrom<HabitVariation>
    {
        public string Name { get; set; }
        public List<HabitDifficultyDto> DifficultiesList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitVariation, HabitVariationDto>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.HabitVariantName))
                .ForMember(d => d.DifficultiesList, opt => opt.MapFrom(s => s.HabitDifficulties));
        }
    }
}
