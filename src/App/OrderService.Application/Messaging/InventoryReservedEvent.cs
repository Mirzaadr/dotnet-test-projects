namespace OrderService.Application.Messaging;

public class InventoryReservedEvent
{
    public Guid OrderId { get; set; }
    public bool Success { get; set; }
}