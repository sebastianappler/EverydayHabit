using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Androidlication.Common.Interfaces;
using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Habits.Queries.GetHabitDetail;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
                var entity = await _context.Habits.FindAsync(request.Id);

                if(entity == null)
                {
                    throw new NotFoundException(nameof(Habit), request.Id);
                }

                return _mapper.Map<HabitDetailVm>(entity);
            }
        }
    }
}
