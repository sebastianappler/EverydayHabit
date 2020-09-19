using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.Habits.Queries.GetHabitsList
{
    public class GetHabitDetailQuery : IRequest<HabitDetailVm>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetHabitDetailQuery, HabitDetailVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<HabitDetailVm> Handle(GetHabitDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.Habits
                    .Where(habit => habit.HabitId == request.Id)
                    .ProjectTo<HabitDetailVm>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken); ;

                if(vm == null)
                {
                    throw new NotFoundException(nameof(Habit), request.Id);
                }

                return vm;
            }
        }
    }
}
