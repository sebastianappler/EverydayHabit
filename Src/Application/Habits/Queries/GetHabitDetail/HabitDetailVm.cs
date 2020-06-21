using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using System.Collections.Generic;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail
{
    public class HabitDetailVm : IMapFrom<Habit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HabitType HabitType { get; set; }
        public List<HabitVariationDto> VariationsList { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, HabitDetailVm>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitId))
                .ForMember(d => d.VariationsList, opt => opt.MapFrom(s => s.Variants));
        }
    }
}
