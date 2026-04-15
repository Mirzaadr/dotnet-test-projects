using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using OrderService.Application.Common.Interfaces;

namespace OrderService.Infrastructure.Messaging;

public class RabbitMqPublisher : IMessagePublisher
{
    private readonly IConnection _connection;

    public RabbitMqPublisher(IConnection connection)
    {
        _connection = connection;
    }

    public Task PublishAsync<T>(T message)
    {
        using var channel = _connection.CreateModel();

        channel.QueueDeclare(
            queue: "order.created",
            durable: true,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: "",
            routingKey: "order.created",
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}