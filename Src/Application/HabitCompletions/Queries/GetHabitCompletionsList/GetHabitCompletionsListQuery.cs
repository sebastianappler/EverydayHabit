using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class GetHabitCompletionsListQuery : IRequest<HabitCompletionsListVm>
    {
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
