using Microsoft.AspNetCore.Mvc;
using CustomerApp.BLL.Customers;
using CustomerApp.DAL.Entities;
using CustomerApp.Api.Contracts;

namespace CustomerApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll([FromServices] GetAllCustomers handler)
    {
        var customers = await handler.ExecuteAsync();

        return Ok(
            GenerateResponse<object>("Success", customers)
        );
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id, [FromServices] GetCustomerById handler)
    {
        var customer = await handler.ExecuteAsync(id);
        if (customer is null)
        {
            return NotFound(GenerateResponse<string?>("Not Found", null));
        }
        return Ok(GenerateResponse<object>("Success", customer));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCustomerRequest customer, [FromServices] CreateCustomer handler)
    {
        var created = await handler.ExecuteAsync(
            customer.code,
            customer.name,
            customer.address);
        return CreatedAtAction(nameof(Get), new { id = created.Customerid }, GenerateResponse<object>("Created", created));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateCustomerRequest customer, [FromServices] UpdateCustomer handler)
    {
        if (id != customer.id)
        {
            return BadRequest(GenerateResponse<string?>("Inconsistent Id", null));
        }
        var newCustomer = await handler.ExecuteAsync(
            customer.id,
            customer.code,
            customer.name,
            customer.address
        );
        return Ok(
            GenerateResponse<object>("Success", newCustomer));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, [FromServices] DeleteCustomer handler)
    {
        await handler.ExecuteAsync(id);
        return Ok(
            GenerateResponse<string>("Success", null)
        );
    }

    private ApiResponse<T> GenerateResponse<T>(string message, T? data)
    {
        var transactionId = HttpContext.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString();
        return new ApiResponse<T>(message, transactionId, data);
    }
}
