using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using OrderService.Application.Common.Interfaces;
using OrderService.Infrastructure.Messaging.Configurations;
using Microsoft.Extensions.Options;

namespace OrderService.Infrastructure.Messaging;

public static class DependencyInjection
{
    public static IServiceCollection AddMessaging(
        this IServiceCollection services,
        IConfiguration config)
    {
        services.Configure<RabbitMqSettings>(config.GetSection(RabbitMqSettings.SectionName));

        services.AddSingleton<IConnection>(sp =>
        {
            var settings = sp
                .GetRequiredService<IOptions<RabbitMqSettings>>()
                .Value;

            var factory = new ConnectionFactory()
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password
            };

            return factory.CreateConnection();
        });

        services.AddSingleton<IMessagePublisher, RabbitMqPublisher>();

        return services;
    }
}