using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using OrderService.Application.Common.Interfaces;
using OrderService.Infrastructure.Messaging;

namespace OrderService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        ConfigurationManager config
    )
    {
        var rabbitConfig = config.GetSection("RabbitMQ");
        var factory = new ConnectionFactory()
        {
            HostName = rabbitConfig["Host"],
            Port = int.Parse(rabbitConfig["Port"]!),
            UserName = rabbitConfig["Username"],
            Password = rabbitConfig["Password"]
        };

        var connection = factory.CreateConnection();

        services.AddSingleton(connection);
        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}