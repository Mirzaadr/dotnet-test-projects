using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using OrderService.Application.Messaging;
using OrderService.Application.Repositories;

namespace OrderWorker;

public class Worker : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IOrderRepository _repository;
    private readonly IServiceScopeFactory _scopeFactory;

    public Worker(
        IConnection connection,
        IOrderRepository repository,
        IServiceScopeFactory scopeFactory)
    {
        _connection = connection;
        _repository = repository;
        _scopeFactory = scopeFactory;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var channel = _connection.CreateModel();

        channel.QueueDeclare("inventory.reserved", true, false, false);

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += async (model, ea) =>
        {
            using var scope = _scopeFactory.CreateScope();

            var repository = scope.ServiceProvider
                .GetRequiredService<IOrderRepository>();

            var json = Encoding.UTF8.GetString(ea.Body.ToArray());
            var evt = JsonSerializer.Deserialize<InventoryReservedEvent>(json);

            Console.WriteLine($"🧾 Updating order {evt!.OrderId}");

            var order = await repository.GetByIdAsync(evt.OrderId);

            if (order == null)
            {
                Console.WriteLine("❌ Order not found");
                return;
            }

            if (evt.Success)
            {
                order.MarkPaid();
                // order.MarkCompleted();
            }
            else
            {
                order.MarkFailed();
            }

            await repository.UpdateAsync(order);

            Console.WriteLine($"✅ Order updated: {order.Id}");
        };

        channel.BasicConsume("inventory.reserved", true, consumer);

        return Task.CompletedTask;
    }
}