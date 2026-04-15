
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

namespace PaymentWorker;

public class Worker : BackgroundService
{
    private readonly IConnection _connection;

    public Worker(IConnection connection)
    {
        _connection = connection;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _connection.CreateModel();

        channel.QueueDeclare("order.created", true, false, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var json = Encoding.UTF8.GetString(body);

            var order = JsonSerializer.Deserialize<OrderCreatedEvent>(json);

            Console.WriteLine($"💰 Processing payment for Order: {order?.OrderId}");
        };

        channel.BasicConsume(queue: "order.created", autoAck: true, consumer: consumer);

        return Task.CompletedTask;
    }
}
