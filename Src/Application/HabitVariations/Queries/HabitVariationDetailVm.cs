﻿using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Domain.Entities;
using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail
{
    public class HabitVariationDetailVm : IMapFrom<HabitVariation>
    {
        public string Name { get; set; }
        public int HabitId { get; set; }
        public List<HabitDifficultyDto> DifficultiesList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitVariation, HabitVariationDetailVm>()
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.HabitVariantName))
                .ForMember(d => d.DifficultiesList, opt => opt.MapFrom(s => s.HabitDifficulties));
        }
    }
}
