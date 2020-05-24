using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitCompletions.Commands.CreateHabitCompletion
{
    public class CreateHabitCompletionCommand : IRequest<int>
    {
        public DateTime Date { get; set; }
        public int CompletedHabitId { get; set; }
        public HabitDifficultyLevel HabitDifficultyLevel { get; set; }

        public class Handler : IRequestHandler<CreateHabitCompletionCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(CreateHabitCompletionCommand request, CancellationToken cancellationToken)
            {
                var habitCompleted = await _context.Habits.FindAsync(request.CompletedHabitId);

                if (habitCompleted == null)
                {
                    throw new NotFoundException(nameof(Habit), request.CompletedHabitId);
                }

                var entity = new HabitCompletion
                {
                    Date = request.Date,
                    HabitDifficultyLevel = request.HabitDifficultyLevel,
                    CompletedHabit = habitCompleted,
                };

                _context.HabitCompletions.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitCompletionCreated { HabitCompletionId = entity.HabitCompletionId }, cancellationToken);

                return entity.HabitCompletionId;
            }
        }
    }
}
