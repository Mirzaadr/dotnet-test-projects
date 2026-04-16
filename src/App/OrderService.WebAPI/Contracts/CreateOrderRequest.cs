namespace OrderService.WebAPI.Contracts;

public class CreateOrderRequest
{
    /// <summary>
    /// Customer email
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Total order amount
    /// </summary>
    public decimal Amount { get; set; }
}