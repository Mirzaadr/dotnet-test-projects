namespace OrderService.Application.Orders.Command.CreateOrder;

public class CreateOrderCommand
{
    public string Email { get; set; } = string.Empty;
    public decimal Amount { get; set; }
}