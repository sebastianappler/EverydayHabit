using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail
{
    public class HabitDifficultyDto : IMapFrom<HabitDifficulty>
    {
        public int Id { get; set; }
        public string Definition { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitDifficulty, HabitDifficultyDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitDifficultyId));
        }
    }
}
