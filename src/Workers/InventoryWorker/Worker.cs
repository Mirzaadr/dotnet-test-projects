using System.Text;
using System.Text.Json;
using OrderService.Application.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryWorker;

// (ILogger<Worker> logger)
public class Worker : BackgroundService
{
    private readonly IConnection _connection;

    public Worker(IConnection connection)
    {
        _connection = connection;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // while (!stoppingToken.IsCancellationRequested)
        // {
        //     if (logger.IsEnabled(LogLevel.Information))
        //     {
        //         logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
        //     }
        //     await Task.Delay(1000, stoppingToken);
        // }

        var channel = _connection.CreateModel();
        channel.QueueDeclare("payment.completed", true, false, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evt = JsonSerializer.Deserialize<PaymentCompletedEvent>(json);

            if (evt!.Success)
            {
                Console.WriteLine($"📦 Updating inventory for Order {evt.OrderId}");
            }
            else
            {
                Console.WriteLine($"❌ Payment failed for Order {evt.OrderId}");
            }
        };

        channel.BasicConsume("payment.completed", true, consumer);

        return Task.CompletedTask;
    }
}
