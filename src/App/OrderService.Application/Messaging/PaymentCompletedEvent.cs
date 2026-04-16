namespace OrderService.Application.Messaging;

public class PaymentCompletedEvent
{
    public Guid OrderId { get; set; }
    public bool Success { get; set; }
}