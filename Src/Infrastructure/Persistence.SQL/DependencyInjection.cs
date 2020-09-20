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
        public static IServiceCollection AddPersistenceSQL(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString(DatabaseConfiguration.EverydayHabitDatabaseName);

            services.AddDbContext<EverydayHabitDbContext>(options =>
                options.UseSqlServer(connectionString, 
                    b => b.MigrationsAssembly("Persistence.SQL")
                )
            );

            services.AddScoped<IEverydayHabitDbContext>(provider => provider.GetService<EverydayHabitDbContext>());

            return services;
        }
    }
}
