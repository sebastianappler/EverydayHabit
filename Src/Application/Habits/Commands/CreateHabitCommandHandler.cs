using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands
{
    public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, int>
    {
        private readonly IEverydayHabitDbContext _context;

        public CreateHabitCommandHandler(IEverydayHabitDbContext EverydayHabitDbContext)
        {
            _context = EverydayHabitDbContext;
        }


        public async Task<int> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
        {
            var entity = new Habit
            {
                Name = request.Name,
            };

            _context.Habits.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.HabitId;
        }
    }
}
