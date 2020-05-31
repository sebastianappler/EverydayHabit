using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitDifficulties.Commands.UpsertHabitDifficulty
{
    public class UpsertHabitDifficultyCommand : IRequest<int>
    {
        public int Id { get; set; }
        public int HabitVariationId { get; set; }
        public string Description { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }

        public class Handler : IRequestHandler<UpsertHabitDifficultyCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpsertHabitDifficultyCommand request, CancellationToken cancellationToken)
            {
                var habitVariation = await _context.HabitVariations.FindAsync(request.HabitVariationId);

                if (habitVariation == null)
                {
                    throw new NotFoundException(nameof(HabitVariation), request.HabitVariationId);
                }

                HabitDifficulty entity;

                if (request.Id != 0)
                {
                    entity = await _context.HabitDifficulties.FindAsync(request.Id);
                    await _mediator.Publish(new HabitDifficultyUpdated { HabitDifficultyId = entity.HabitDifficultyId }, cancellationToken);
                }
                else
                {
                    entity = new HabitDifficulty();
                    _context.HabitDifficulties.Add(entity);
                    await _mediator.Publish(new HabitDifficultyCreated { HabitDifficultyId = entity.HabitDifficultyId }, cancellationToken);
                }

                entity.HabitVariationId = request.HabitVariationId;
                entity.Description = request.Description;
                entity.DifficultyLevel = request.DifficultyLevel;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.HabitDifficultyId;
            }
        }
    }
}
