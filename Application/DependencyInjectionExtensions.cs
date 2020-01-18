using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application
{
    public static class DependencyInjectionExtensions
    {
        
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}