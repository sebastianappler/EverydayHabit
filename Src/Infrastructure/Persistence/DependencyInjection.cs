using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EverydayHabit.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("EverydayHabitDatabase");
            var databaseProvider = configuration.GetSection(nameof(DatabaseProviderConfiguration)).Get<DatabaseProviderConfiguration>();

            switch (databaseProvider.ProviderType)
            {
                case DatabaseProviderType.Sqlite:
                    services.AddDbContext<EverydayHabitDbContext>(options =>
                        options.UseSqlite(connectionString));

                    SQLitePCL.Batteries.Init();
                    break;
                case DatabaseProviderType.SqlServer:
                    services.AddDbContext<EverydayHabitDbContext>(options =>
                        options.UseSqlServer(connectionString));
                    break;

            }
           
            services.AddScoped<IEverydayHabitDbContext>(provider => provider.GetService<EverydayHabitDbContext>());

            return services;
        }
    }
}
