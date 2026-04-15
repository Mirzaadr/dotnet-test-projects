using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Messaging;

namespace OrderService.WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private readonly IMessagePublisher _publisher;

    public OrderController(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    [HttpPost]
    public async Task<IActionResult> Create()
    {
        var evt = new OrderCreatedEvent
        {
            OrderId = Guid.NewGuid(),
            Email = "test@email.com",
            Amount = 100
        };

        await _publisher.PublishAsync(evt);

        return Ok("Order event published");
    }
}