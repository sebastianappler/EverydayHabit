using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class DeleteHabitCompletionCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteHabitCompletionCommand>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteHabitCompletionCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.HabitCompletions.FirstOrDefaultAsync(h => h.HabitCompletionId == request.Id, cancellationToken);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(HabitCompletion), request.Id);
                }

                _context.HabitCompletions.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
