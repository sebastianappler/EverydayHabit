using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class CreateHabitVariationCommand : IRequest<int>
    {
        public int HabitId { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<CreateHabitVariationCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(CreateHabitVariationCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Habits.FindAsync(request.HabitId);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(Habit), request.HabitId);
                }

                var habitVariation = new HabitVariation
                {
                    HabitId = request.HabitId,
                    HabitVariantName = request.Name
                };

                entity.Variants.Add(habitVariation);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitVariationCreated { HabitVariationId = habitVariation.HabitVariationId }, cancellationToken);

                return habitVariation.HabitVariationId;
            }
        }
    }
}
