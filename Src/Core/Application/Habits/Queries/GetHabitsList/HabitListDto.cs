using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class HabitListDto : IMapFrom<Habit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HabitType HabitType { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, HabitListDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitId));
        }
    }
}
