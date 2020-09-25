using AutoMapper;
using AutoMapper.QueryableExtensions;
using EverydayHabit.Application.Common.Exceptions;
using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation
{
    public class GetHabitDifficultyDetailQuery : IRequest<HabitDifficultyDetailVm>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetHabitDifficultyDetailQuery, HabitDifficultyDetailVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<HabitDifficultyDetailVm> Handle(GetHabitDifficultyDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.HabitDifficulties
                    .Where(habitDifficulty => habitDifficulty.HabitDifficultyId == request.Id)
                    .ProjectTo<HabitDifficultyDetailVm>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken); ;

                if (vm == null)
                {
                    throw new NotFoundException(nameof(HabitDifficulty), request.Id);
                }

                return vm;
            }
        }
    }
}
