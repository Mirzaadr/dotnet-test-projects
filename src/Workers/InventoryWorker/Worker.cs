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
    private readonly IMessagePublisher _publisher;
    private readonly IMessageConsumer _consumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(
        IMessagePublisher publisher, 
        IMessageConsumer consumer, 
        IServiceScopeFactory scopeFactory
    )
    {
        _publisher = publisher;
        _consumer = consumer;
        _scopeFactory = scopeFactory;
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Consume<PaymentCompletedEvent>(
            queueName: "payment.completed",
            handler: async (message, sp) =>
            {
                if (message.Success)
                {
                    Console.WriteLine($"📦 Reserving inventory for {message.OrderId}");

                    await Task.Delay(1000, stoppingToken); // simulate

                    await _publisher.PublishAsync(new InventoryReservedEvent
                    {
                        OrderId = message.OrderId,
                        Success = true
                    });

                    Console.WriteLine($"✅ Inventory reserved for {message.OrderId}");
                }
                else
                {
                    Console.WriteLine($"❌ Payment failed for Order {message.OrderId}");
                }
            },
            scopeFactory: _scopeFactory
        );

        return Task.CompletedTask;
    }
}
