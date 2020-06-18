using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class GetHabitCompletionsListQuery : IRequest<HabitCompletionsListVm>
    {
        public int HabitId { get; set; }
        public class Handler : IRequestHandler<GetHabitCompletionsListQuery, HabitCompletionsListVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<HabitCompletionsListVm> Handle(GetHabitCompletionsListQuery request, CancellationToken cancellationToken)
            {
                var habitCompletions = await _context.HabitCompletions
                    .Where(hc => hc.Habit.HabitId == request.HabitId)
                    .ProjectTo<HabitCompletionsListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var vm = new HabitCompletionsListVm
                {
                    HabitCompletionsList = habitCompletions
                };

                return vm;
            }
        }
    }
}
