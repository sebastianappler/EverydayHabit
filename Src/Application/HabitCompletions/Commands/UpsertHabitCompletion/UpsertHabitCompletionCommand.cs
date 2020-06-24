using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitCompletions.Commands.UpsertHabitCompletion
{
    public class UpsertHabitCompletionCommand : IRequest<int>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int HabitId { get; set; }
        public int HabitVariationId { get; set; }
        public HabitDifficultyLevel HabitDifficultyLevel { get; set; }

        public class Handler : IRequestHandler<UpsertHabitCompletionCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpsertHabitCompletionCommand request, CancellationToken cancellationToken)
            {
                var habitCompleted = await _context.Habits.FindAsync(request.HabitId);
                if (habitCompleted == null)
                {
                    throw new NotFoundException(nameof(Habit), request.HabitId);
                }

                var habitVariationCompleted = habitCompleted.Variants.SingleOrDefault(h => h.HabitVariationId == request.HabitVariationId);
                if (habitVariationCompleted == null)
                {
                    throw new NotFoundException(nameof(HabitVariation), request.HabitVariationId);
                }

                HabitCompletion entity;

                if (request.Id > 0)
                {
                    entity = await _context.HabitCompletions.FindAsync(request.Id);
                    await _mediator.Publish(new HabitCompletionUpdated { HabitCompletionId = entity.HabitCompletionId }, cancellationToken);
                }
                else
                {
                    entity = new HabitCompletion();
                    _context.HabitCompletions.Add(entity);
                    await _mediator.Publish(new HabitCompletionCreated { HabitCompletionId = entity.HabitCompletionId }, cancellationToken);
                }

                entity.Date = request.Date;
                entity.HabitDifficultyLevel = request.HabitDifficultyLevel;
                entity.Habit = habitCompleted;
                entity.HabitVariationId = request.HabitVariationId;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.HabitCompletionId;
            }
        }
    }
}
