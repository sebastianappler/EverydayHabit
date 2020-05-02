using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Androidlication.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsListQuery
{
    public class GetHabitsListQuery : IRequest<GetHabitsListVm>
    {
        public class Handler : IRequestHandler<GetHabitsListQuery, GetHabitsListVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<GetHabitsListVm> Handle(GetHabitsListQuery request, CancellationToken cancellationToken)
            {
                var habits = await _context.Habits
                    .ProjectTo<GetHabitsListDto>(_mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var vm = new GetHabitsListVm
                {
                    Habits = habits
                };

                return vm;
            }
        }
    }
}
