using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;

namespace EverydayHabit.Application.Habits.Queries.GetHabitDetail
{
    public class HabitVariantDto : IMapFrom<HabitVariation>
    {
        public int Id { get; set; }
        public HabitDifficultyDto Mini { get; set; }
        public HabitDifficultyDto Plus { get; set; }
        public HabitDifficultyDto Elite { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<HabitVariation, HabitVariantDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitVariationId));
        }
    }
}
