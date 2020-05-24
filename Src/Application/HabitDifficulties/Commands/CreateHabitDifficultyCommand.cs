using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
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
                var entity = await _context.HabitVariations.FindAsync(request.HabitVariationId);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(HabitVariation), request.HabitVariationId);
                }

                var habitDifficulty = new HabitDifficulty
                {
                    Description = request.Description,
                    DifficultyLevel = request.DifficultyLevel
                };

                entity.HabitDifficulties.Add(habitDifficulty);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitDifficultyCreated { HabitDifficultyId = habitDifficulty.HabitDifficultyId }, cancellationToken);

                return entity.HabitId;
            }
        }
    }
}
