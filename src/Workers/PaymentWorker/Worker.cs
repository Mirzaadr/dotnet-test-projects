
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
    private readonly IMessagePublisher _messagePublisher;
    private readonly IMessageConsumer _messageConsumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(
        IMessagePublisher messagePublisher,
        IMessageConsumer messageConsumer,
        IServiceScopeFactory scopeFactory)
    {
        _messagePublisher = messagePublisher;
        _messageConsumer = messageConsumer;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageConsumer.Consume<OrderCreatedEvent>(
            queueName: "order.created",
            handler: async (message, sp) =>
            {
                Console.WriteLine($"💰 Processing {message.OrderId}");

                await Task.Delay(1000, stoppingToken);

                await _messagePublisher.PublishAsync(new PaymentCompletedEvent
                {
                    OrderId = message.OrderId,
                    Success = true
                });

                Console.WriteLine($"✅ Payment completed {message.OrderId}");
            },
            scopeFactory: _scopeFactory
        );

        return Task.CompletedTask;
    }
}
