using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitDifficulties.Commands.CreateHabitDifficulty
{
    public class CreateHabitDifficultyCommand : IRequest<int>
    {
        public int HabitVariationId { get; set; }
        public string Description { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }

        public class Handler : IRequestHandler<CreateHabitDifficultyCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(CreateHabitDifficultyCommand request, CancellationToken cancellationToken)
            {
                var habitVariation = await _context.HabitVariations.FindAsync(request.HabitVariationId);

                if (habitVariation == null)
                {
                    throw new NotFoundException(nameof(HabitVariation), request.HabitVariationId);
                }

                var entity = new HabitDifficulty
                {
                    HabitVariationId = request.HabitVariationId,
                    Description = request.Description,
                    DifficultyLevel = request.DifficultyLevel
                };

                _context.HabitDifficulties.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitDifficultyCreated { HabitDifficultyId = entity.HabitDifficultyId }, cancellationToken);

                return entity.HabitDifficultyId;
            }
        }
    }
}
