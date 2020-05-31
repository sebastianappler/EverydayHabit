using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitVariations.Commands.UpsertHabitVariation
{
    public class UpsertHabitVariationCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int HabitId { get; set; }
        public string Name { get; set; }

        public class Handler : IRequestHandler<UpsertHabitVariationCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpsertHabitVariationCommand request, CancellationToken cancellationToken)
            {
                var habit = await _context.Habits.FindAsync(request.HabitId);

                if (habit == null)
                {
                    throw new NotFoundException(nameof(Habit), request.HabitId);
                }

                HabitVariation entity;

                if (request.Id != 0)
                {
                    entity = await _context.HabitVariations.FindAsync(request.Id);
                }
                else
                {
                    entity = new HabitVariation();

                    _context.HabitVariations.Add(entity);
                    await _mediator.Publish(new HabitVariationCreated { HabitVariationId = entity.HabitVariationId }, cancellationToken);
                }

                entity.HabitId = request.HabitId;
                entity.HabitVariantName = request.Name;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.HabitVariationId;
            }
        }
    }
}
