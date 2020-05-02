using AutoMapper;
using EverydayHabit.Application.Common.Mapping;
using EverydayHabit.Persistence;
using System;
using Xunit;

namespace Application.IntegrationTests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public EverydayHabitDbContext Context { get; private set; }
        public IMapper Mapper { get; private set; }

        public QueryTestFixture()
        {
            Context = EverydayHabitContextFactory.Create();

            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

            Mapper = configurationProvider.CreateMapper();
        }

        public void Dispose()
        {
            EverydayHabitContextFactory.Destroy(Context);
        }
    }

    [CollectionDefinition("QueryCollection")]
    public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
}
