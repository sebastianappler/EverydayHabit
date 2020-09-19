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

namespace EverydayHabit.Application.HabitVariations.Queries.GetHabitVariation
{
    public class GetHabitVariationDetailQuery : IRequest<HabitVariationDetailVm>
    {
        public int Id { get; set; }

        public class Handler : IRequestHandler<GetHabitVariationDetailQuery, HabitVariationDetailVm>
        {
            private readonly IEverydayHabitDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IEverydayHabitDbContext everydayHabitDbContext, IMapper mapper)
            {
                _context = everydayHabitDbContext;
                _mapper = mapper;
            }

            public async Task<HabitVariationDetailVm> Handle(GetHabitVariationDetailQuery request, CancellationToken cancellationToken)
            {
                var vm = await _context.HabitVariations
                    .Where(habitVariation => habitVariation.HabitVariationId == request.Id)
                    .ProjectTo<HabitVariationDetailVm>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(cancellationToken); ;

                if (vm == null)
                {
                    throw new NotFoundException(nameof(HabitVariation), request.Id);
                }

                return vm;
            }
        }
    }
}
