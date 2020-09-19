using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class DeleteHabitCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteHabitCommand>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteHabitCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Habits.FirstOrDefaultAsync(h => h.HabitId == request.Id, cancellationToken);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(Habit), request.Id);
                }

                _context.Habits.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
