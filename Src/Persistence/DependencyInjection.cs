using EverydayHabit.Application.Common.Interfaces;
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
            services.AddDbContext<EverydayHabitDbContext>(options =>
                options.UseSqlite(connectionString));
            SQLitePCL.Batteries.Init();
            services.AddScoped<IEverydayHabitDbContext>(provider => provider.GetService<EverydayHabitDbContext>());

            return services;
        }
    }
}
