using ElasticHabitCalendar.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ElasticHabitCalendar.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ElasticHabitCalendarDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("ElasticHabitCalendarDatabase")));

            services.AddScoped<IElasticHabitCalendarDbContext>(provider => provider.GetService<ElasticHabitCalendarDbContext>());

            return services;
        }
    }
}
