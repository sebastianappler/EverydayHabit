using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using System;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class HabitCompletionsListDto : IMapFrom<HabitCompletion>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public Habit CompletedHabit { get; set; }
        public HabitDifficultyLevel HabitDifficultyLevel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitCompletion, HabitCompletionsListDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitCompletionId));
        }
    }
}
