using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitVariations.Commands.DeleteHabitVariation
{
    public class DeleteHabitVariationCommand : IRequest
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<DeleteHabitVariationCommand>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<Unit> Handle(DeleteHabitVariationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.HabitVariations.FirstOrDefaultAsync(hv => hv.HabitVariationId == request.Id, cancellationToken);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Habit), request.Id);
                }

                _context.HabitVariations.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
