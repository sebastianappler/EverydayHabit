using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class UpdateHabitDifficultyCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public HabitDifficultyLevel DifficultyLevel { get; set; }

        public class Handler : IRequestHandler<UpdateHabitDifficultyCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpdateHabitDifficultyCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.HabitDifficulties.FirstOrDefaultAsync(h => h.HabitDifficultyId == request.Id, cancellationToken);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(HabitDifficulty), request.Id);
                }

                entity.Description = request.Description;
                entity.DifficultyLevel = request.DifficultyLevel;
                
                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitDifficultyUpdated { HabitDifficultyId = entity.HabitDifficultyId }, cancellationToken);

                return entity.HabitDifficultyId;
            }
        }
    }
}
