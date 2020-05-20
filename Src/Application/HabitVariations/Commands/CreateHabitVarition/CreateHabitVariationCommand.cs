using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class CreateHabitVariationCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }

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
                var entity = new HabitVariation
                {
                    
                };

                _context.HabitVariations.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitVariationCreated { HabitVariationId = entity.HabitVariantId }, cancellationToken);

                return entity.HabitId;
            }
        }
    }
}
