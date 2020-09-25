using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using System;

namespace EverydayHabit.Application.HabitCompletions.Queries.GetHabitCompletionsList.Dtos
{
    public class HabitCompletionsListDto : IMapFrom<HabitCompletion>
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public int HabitVariationId { get; set; }
        public int HabitDifficultyId { get; set; }

        public DateTime Date { get; set; }
        public HabitCompletionHabitDto Habit { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitCompletion, HabitCompletionsListDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitCompletionId))
                ;
        }
    }
}
