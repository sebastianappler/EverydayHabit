using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using System;

namespace EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos
{
    public class HabitCompletionDifficultyDto : IMapFrom<HabitDifficulty>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitDifficulty, HabitCompletionDifficultyDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitDifficultyId))
                ;
        }
    }
}
