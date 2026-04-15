using OrderService.Domain.Common;

namespace OrderService.Domain.Events;

public class OrderCreatedDomainEvent : IDomainEvent
{
    public Guid OrderId { get; }

    public OrderCreatedDomainEvent(Guid orderId)
    {
        OrderId = orderId;
    }
}