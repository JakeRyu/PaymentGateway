using Application.Common.Interfaces;
using Bank.ApiClients;
using Bank.Simulator;
using Microsoft.Extensions.DependencyInjection;

namespace Bank
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, bool isDevelopment)
        {
            if (isDevelopment)    // Bank simulator is only used in Development environment
            {
                services.AddScoped<IAcquireBank, AcquireBankSimulator>();
            }
            else
            {
                services.AddScoped<IAcquireBank, AcquireBank>();
            }

            return services;
        }
    }
}