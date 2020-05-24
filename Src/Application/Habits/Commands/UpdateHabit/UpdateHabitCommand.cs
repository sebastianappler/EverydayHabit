using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands.CreateHabit
{
    public class UpdateHabitCommand : IRequest<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<UpdateHabitCommand, int>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMediator _mediator;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMediator mediator)
            {
                _context = everydayHabitDbContext;
                _mediator = mediator;
            }

            public async Task<int> Handle(UpdateHabitCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Habits.FirstOrDefaultAsync(h => h.HabitId == request.Id, cancellationToken);

                entity.Name = request.Name;
                entity.Description = request.Description;
                
                await _context.SaveChangesAsync(cancellationToken);
                await _mediator.Publish(new HabitUpdated { HabitId = entity.HabitId }, cancellationToken);

                return entity.HabitId;
            }
        }
    }
}
