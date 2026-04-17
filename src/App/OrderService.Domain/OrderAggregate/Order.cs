namespace OrderService.Domain.OrderAggregate;

public class Order
{
    public Guid Id { get; private set; }
    public string CustomerEmail { get; private set; } = string.Empty;
    public decimal TotalAmount { get; private set; }
    public OrderStatus Status { get; private set; }

    private Order() { } // EF

    public Order(string email, decimal total)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email is required");

        if (total <= 0)
            throw new ArgumentException("Total must be greater than zero");

        Id = Guid.NewGuid();
        CustomerEmail = email;
        TotalAmount = total;
        Status = OrderStatus.Created;
    }

    public void MarkPaid()
    {
        if (Status == OrderStatus.Paid)
            return;
        
        if (Status != OrderStatus.Created)
            throw new InvalidOperationException("Order cannot be paid");

        Status = OrderStatus.Paid;
    }

    public void MarkFailed()
    {
        if (Status == OrderStatus.Failed)
            return;
        
        if (Status == OrderStatus.Completed)
            throw new InvalidOperationException("Completed order cannot fail");

        Status = OrderStatus.Failed;
    }

    public void MarkCompleted()
    {
        if (Status == OrderStatus.Completed)
            return;
        
        if (Status != OrderStatus.Paid)
            throw new InvalidOperationException("Order must be paid before completion");

        Status = OrderStatus.Completed;
    }
}