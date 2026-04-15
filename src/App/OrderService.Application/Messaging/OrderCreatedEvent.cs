namespace OrderService.Application.Messaging;

public class OrderCreatedEvent
{
    public Guid OrderId { get; set; }
    public string Email { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}