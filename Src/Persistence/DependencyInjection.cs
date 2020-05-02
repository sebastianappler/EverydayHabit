using EverydayHabit.Androidlication.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EverydayHabit.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EverydayHabitDbContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("EverydayHabitDatabase")));

            services.AddScoped<IEverydayHabitDbContext>(provider => provider.GetService<EverydayHabitDbContext>());

            return services;
        }
    }
}
