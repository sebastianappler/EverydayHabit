using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Androidlication.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class GetHabitsListQuery : IRequest<HabitsListVm>
    {
        public class Handler : IRequestHandler<GetHabitsListQuery, HabitsListVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<HabitsListVm> Handle(GetHabitsListQuery request, CancellationToken cancellationToken)
            {
                var habits = await _context.Habits
                    .ProjectTo<HabitListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var vm = new HabitsListVm
                {
                    Habits = habits
                };

                return vm;
            }
        }
    }
}
