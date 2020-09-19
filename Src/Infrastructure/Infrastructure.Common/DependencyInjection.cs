using Application.Common.Interfaces;
using EverydayHabit.Application.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EverydayHabit.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureCommon(this IServiceCollection services)
        {
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();

            return services;
        }
    }
}
