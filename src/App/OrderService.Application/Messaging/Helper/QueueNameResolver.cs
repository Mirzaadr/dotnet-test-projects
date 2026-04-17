namespace OrderService.Application.Messaging.Helper;

public static class QueueNameResolver
{
    public static string GetQueueName<T>()
    {
        return typeof(T).Name switch
        {
            nameof(OrderCreatedEvent) => "order.created",
            nameof(PaymentCompletedEvent) => "payment.completed",
            nameof(InventoryReservedEvent) => "inventory.reserved",
            _ => throw new Exception("Unknown event")
        };
    }
}