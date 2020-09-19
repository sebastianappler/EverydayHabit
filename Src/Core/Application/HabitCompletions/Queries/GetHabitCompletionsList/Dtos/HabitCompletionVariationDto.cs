using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using System;

namespace EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos
{
    public class HabitCompletionVariationDto : IMapFrom<HabitVariation>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitVariation, HabitCompletionVariationDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitVariationId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.HabitVariantName)
                );
        }
    }
}
