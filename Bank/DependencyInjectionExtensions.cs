using Application.Common.Interfaces;
using Bank.ApiClients;
using Bank.Simulator;
using Microsoft.Extensions.DependencyInjection;

namespace Bank
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddBank(this IServiceCollection services, bool isDevelopment)
        {
            if (isDevelopment)    // Bank simulator is only used in Development environment
            {
                services.AddScoped<IBankClientFactory, BankClientFactorySimulator>();
            }
            else
            {
                services.AddScoped<IBankClientFactory, BankClientFactory>();
            }

            return services;
        }
    }
}