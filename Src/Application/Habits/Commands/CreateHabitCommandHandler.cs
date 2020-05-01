using ElasticHabitCalendar.AndroidApplication.Common.Interfaces;
using ElasticHabitCalendar.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Habits.Commands
{
    public class CreateHabitCommandHandler : IRequestHandler<CreateHabitCommand, int>
    {
        private readonly IElasticHabitCalendarDbContext _context;

        public CreateHabitCommandHandler(IElasticHabitCalendarDbContext elasticHabitCalendarDbContext)
        {
            _context = elasticHabitCalendarDbContext;
        }


        public async Task<int> Handle(CreateHabitCommand request, CancellationToken cancellationToken)
        {
            var entity = new Habit
            {
                Name = request.Name,
            };

            _context.Habits.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}
