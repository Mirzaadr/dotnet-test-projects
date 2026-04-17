using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Common.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Infrastructure.Messaging;

public class RabbitMqConsumer : IMessageConsumer
{
    private readonly IConnection _connection;

    public RabbitMqConsumer(IConnection connection)
    {
        _connection = connection;
    }

    public void Consume<T>(
        string queueName,
        Func<T, IServiceProvider, Task> handler,
        IServiceScopeFactory scopeFactory)
    {
        var channel = _connection.CreateModel();
        channel.BasicQos(0, 1, false);

        RabbitMqTopology.ConfigureQueue(channel, queueName);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            using var scope = scopeFactory.CreateScope();

            try
            {
                var json = Encoding.UTF8.GetString(ea.Body.ToArray());
                var message = JsonSerializer.Deserialize<T>(json);

                if (message == null)
                {
                    channel.BasicAck(ea.DeliveryTag, false);
                    return;
                }

                await handler(message, scope.ServiceProvider);

                channel.BasicAck(ea.DeliveryTag, false);
            }
            catch
            {
                channel.BasicNack(ea.DeliveryTag, false, false);
            }
        };

        channel.BasicConsume(queueName, false, consumer);
    }
}