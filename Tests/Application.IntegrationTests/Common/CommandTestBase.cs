using EverydayHabit.Persistence;
using System;

namespace Application.IntegrationTests.Common
{
    public class CommandTestBase : IDisposable
    {
        protected readonly EverydayHabitDbContext _context;

        public CommandTestBase()
        {
            _context = EverydayHabitContextFactory.Create();
        }

        public void Dispose()
        {
            EverydayHabitContextFactory.Destroy(_context);
        }
    }
}
