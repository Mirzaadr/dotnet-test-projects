using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Common.Interfaces;
using OrderService.Application.Messaging;
using OrderService.Application.Orders.Command.CreateOrder;
using OrderService.WebAPI.Contracts;

namespace OrderService.WebAPI.Controllers;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    // private readonly ISender _mediator;
    private readonly CreateOrderCommandHandler _handler;

    public OrderController(CreateOrderCommandHandler handler)
    {
        // _mediator = mediator;
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand
        {
            Email = request.Email,
            Amount = request.Amount
        };
        // var orderId = await _mediator.Send(command);
        var orderId = await _handler.Handle(command);

        return Ok(new { OrderId = orderId });
    }
}