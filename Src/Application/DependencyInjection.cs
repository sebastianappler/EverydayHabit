using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using MediatR;

namespace ElasticHabitCalendar.AndroidApplication
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
