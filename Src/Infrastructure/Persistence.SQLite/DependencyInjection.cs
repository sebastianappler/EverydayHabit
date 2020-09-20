using EverydayHabit.Application.Common.Interfaces;
using EverydayHabit.Persistence;
using EverydayHabit.Persistence.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.SQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistenceSQLite(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DatabaseConfiguration.EverydayHabitDatabaseName);
            services.AddDbContext<EverydayHabitDbContext>(options =>
                options.UseSqlite(connectionString,
                    b => b.MigrationsAssembly("Persistence.SQLite")
                )
            );

            SQLitePCL.Batteries.Init();

            services.AddScoped<IEverydayHabitDbContext>(provider => provider.GetService<EverydayHabitDbContext>());

            return services;
        }
    }
}
