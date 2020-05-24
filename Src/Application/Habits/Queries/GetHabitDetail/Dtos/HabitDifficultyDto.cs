using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail.Dtos
{
    public class HabitDifficultyDto : IMapFrom<HabitDifficulty>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitDifficulty, HabitDifficultyDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitDifficultyId));
        }
    }
}
