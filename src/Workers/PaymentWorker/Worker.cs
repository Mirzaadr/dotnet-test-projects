
// public class Worker(ILogger<Worker> logger) : BackgroundService
// {
//     protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//     {
//         while (!stoppingToken.IsCancellationRequested)
//         {
//             if (logger.IsEnabled(LogLevel.Information))
//             {
//                 logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
//             }
//             await Task.Delay(1000, stoppingToken);
//         }
//     }
// }

using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OrderService.Application.Messaging;
using OrderService.Application.Common.Interfaces;

namespace PaymentWorker;

public class Worker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IMessagePublisher _messagePublisher;

    public Worker(IConnection connection, IMessagePublisher messagePublisher)
    {
        _connection = connection;
        _messagePublisher = messagePublisher;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _connection.CreateModel();

        channel.QueueDeclare("order.created", true, false, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);
            var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

            Console.WriteLine($"💰 Processing payment for Order: {order?.OrderId}");

            // simulate success
            await Task.Delay(1000);

            var success = true;

            // publish next event
            await _messagePublisher.PublishAsync(new PaymentCompletedEvent
            {
                OrderId = order!.OrderId,
                Success = success
            });

            Console.WriteLine($"✅ Payment completed for {order.OrderId}");
        };

        channel.BasicConsume(queue: "order.created", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
