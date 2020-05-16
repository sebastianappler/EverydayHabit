using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EverydayHabit.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrasctructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
