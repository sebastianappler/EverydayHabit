using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using System;

namespace EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos
{
    public class HabitCompletionHabitDto : IMapFrom<Habit>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, HabitCompletionHabitDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitId))
                .ForMember(d => d.Name, opt => opt.MapFrom(s => s.Name))
                ;
        }
    }
}
