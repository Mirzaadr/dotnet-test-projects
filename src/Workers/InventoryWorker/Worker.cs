using System.Text;
using System.Text.Json;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Messaging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace InventoryWorker;

// (ILogger<Worker> logger)
public class Worker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IMessagePublisher _publisher;

    public Worker(IConnection connection, IMessagePublisher publisher)
    {
        _connection = connection;
        _publisher = publisher;
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

        consumer.Received += async (model, ea) =>
        {
            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evt = JsonSerializer.Deserialize<PaymentCompletedEvent>(json);

            if (evt!.Success)
            {
                Console.WriteLine($"📦 Reserving inventory for {evt.OrderId}");

                await Task.Delay(1000); // simulate

                await _publisher.PublishAsync(new InventoryReservedEvent
                {
                    OrderId = evt.OrderId,
                    Success = true
                });

                Console.WriteLine($"✅ Inventory reserved for {evt.OrderId}");
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
