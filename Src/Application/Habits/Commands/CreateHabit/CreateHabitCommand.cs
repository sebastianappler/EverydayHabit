using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class CreateHabitCommand : IRequest<int>
    {
        public string Name { get; set; }
        public class Handler : IRequestHandler<CreateHabitCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext EverydayHabitDbContext, IMediator mediator)
            {
                _context = EverydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
            {
                var entity = new Habit
                {
                    Name = request.Name,
                };

                _context.Habits.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitCreated { HabitId = entity.HabitId }, cancellationToken);

                return entity.HabitId;
            }
        }
    }
}
