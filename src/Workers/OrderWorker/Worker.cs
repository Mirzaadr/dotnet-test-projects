using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OrderService.Application.Messaging;
using OrderService.Application.Repositories;
using OrderService.Application.Common.Interfaces;

namespace OrderWorker;

public class Worker : BackgroundService
{
    private readonly IMessageConsumer _messageConsumer;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(
        IMessageConsumer consumer, 
        IServiceScopeFactory scopeFactory
    )
    {
        _messageConsumer = consumer;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _messageConsumer.Consume<InventoryReservedEvent>(
            queueName: "inventory.reserved",
            handler: async (message, sp) =>
            {
                var repository = sp
                    .GetRequiredService<IOrderRepository>();

                Console.WriteLine($"🧾 Updating order {message.OrderId}");

                var order = await repository.GetByIdAsync(message.OrderId);

                if (order == null)
                {
                    throw new Exception("Order not exist");
                }

                if (message.Success)
                {
                    // Console.WriteLine($"✅ Order Paid: {evt.OrderId}");
                    // order.MarkPaid();
                    order.MarkCompleted();
                }
                else
                {
                    // Console.WriteLine($"❌ Order update Fail: {evt.OrderId}");
                    order.MarkFailed();
                }

                await repository.UpdateAsync(order);
            },
            scopeFactory: _scopeFactory
        );

        return Task.CompletedTask;
    }
}