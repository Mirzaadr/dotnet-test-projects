namespace OrderService.Domain.OrderAggregate;

public enum OrderStatus
{
    Created = 1,
    Paid = 2,
    Failed = 3,
    Completed = 4
}