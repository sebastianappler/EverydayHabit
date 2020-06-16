using Application.Habits.Commands.CreateHabit;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using EverydayHabit.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Commands.UpsertHabit
{
    public class UpsertHabitCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public HabitType HabitType { get; set; }

        public class Handler : IRequestHandler<UpsertHabitCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpsertHabitCommand request, CancellationToken cancellationToken)
            {
                Habit entity;

                if (request.Id != 0)
                {
                    entity = await _context.Habits.FirstOrDefaultAsync(h => h.HabitId == request.Id, cancellationToken);
                    await _mediator.Publish(new HabitUpdated { HabitId = entity.HabitId }, cancellationToken);
                }
                else
                {
                    entity = new Habit();
                    _context.Habits.Add(entity);
                    await _mediator.Publish(new HabitCreated { HabitId = entity.HabitId }, cancellationToken);
                }

                entity.Name = request.Name;
                entity.Description = request.Description;
                entity.HabitType = request.HabitType;

                await _context.SaveChangesAsync(cancellationToken);

                return entity.HabitId;
            }
        }
    }
}
