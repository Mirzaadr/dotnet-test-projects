using OrderService.Domain.OrderAggregate;
using OrderService.Application.Repositories;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Messaging;

namespace OrderService.Application.Orders.Command.CreateOrder;

public class CreateOrderCommandHandler
{
    private readonly IOrderRepository _repository;
    private readonly IMessagePublisher _publisher;

    public CreateOrderCommandHandler(
        IOrderRepository repository,
        IMessagePublisher publisher)
    {
        _repository = repository;
        _publisher = publisher;
    }

    public async Task<Guid> Handle(CreateOrderCommand command)
    {
        // 1. Domain logic
        var order = new Order(command.Email, command.Amount);

        // 2. Persist
        await _repository.AddAsync(order);

        // 3. Publish event
        await _publisher.PublishAsync(new OrderCreatedEvent
        {
            OrderId = order.Id,
            Email = order.CustomerEmail,
            Amount = order.TotalAmount
        });

        return order.Id;
    }
}