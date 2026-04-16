using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Infrastructure.Persistence;
using OrderService.Infrastructure.Messaging;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager config)
    {
        services
            .AddPersistence(config)
            .AddMessaging(config);

        return services;
    }
}