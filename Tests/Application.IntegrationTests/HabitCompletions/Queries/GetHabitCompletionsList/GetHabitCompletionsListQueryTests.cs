using Application.IntegrationTests.Common;
using AutoMapper;
using EverydayHabit.Application.Habits.Queries.GetHabitsList;
using EverydayHabit.Persistence;
using Shouldly;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.IntegrationTests.HabitCompletions.Queries.GetHabitCompletionsList
{
    [Collection("QueryCollection")]
    public class GetHabitCompletionsListQueryTests
    {
        private readonly EverydayHabitDbContext _context;
        private readonly IMapper _mapper;

        public GetHabitCompletionsListQueryTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }
    }
}
