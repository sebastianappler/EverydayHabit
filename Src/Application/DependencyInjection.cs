using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using AutoMapper;
using MediatR;

namespace EverydayHabit.Androidlication
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
