using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Domain.Entities;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsListQuery
{
    public class GetHabitsListDto : IMapFrom<Habit>
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Habit, GetHabitsListDto>()
                .ForMember(d => d.Id, opt => opt.MapFrom(s => s.HabitId));
        }
    }
}
