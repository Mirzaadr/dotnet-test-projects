using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Messaging;

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

        var queueName = typeof(T).Name switch
        {
            nameof(OrderCreatedEvent) => "order.created",
            nameof(PaymentCompletedEvent) => "payment.completed",
            _ => throw new Exception("Unknown event type")
        };


        channel.QueueDeclare(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false);

        var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

        channel.BasicPublish(
            exchange: "",
            routingKey: queueName,
            basicProperties: null,
            body: body);

        return Task.CompletedTask;
    }
}