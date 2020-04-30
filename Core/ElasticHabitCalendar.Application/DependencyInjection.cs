using Microsoft.Extensions.DependencyInjection;

namespace ElasticHabitCalendar.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //TODO Add mediator and automapper
            return services;
        }
    }
}
