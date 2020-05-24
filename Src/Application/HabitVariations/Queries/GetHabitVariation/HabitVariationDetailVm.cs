using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Domain.Entities;
using System.Collections.Generic;

namespace EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation
{
    public class HabitVariationDetailVm : IMapFrom<HabitVariation>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HabitId { get; set; }
        public List<HabitDifficultyDto> DifficultiesList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitVariation, HabitVariationDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitVariationId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.HabitVariantName))
                .ForMember(d => d.DifficultiesList, opt => opt.MapFrom(s => s.HabitDifficulties));
        }
    }
}
