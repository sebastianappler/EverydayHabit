using MediatR;

namespace Application.Habits.Commands
{
    public class CreateHabitCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
