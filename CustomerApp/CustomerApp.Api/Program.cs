using CustomerApp.Api.Contracts;
using CustomerApp.Api.Middleware;
using CustomerApp.BLL;
using CustomerApp.DAL;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var transactionId = context.HttpContext.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString();

        var errors = context.ModelState
            .Where(e => e.Value?.Errors.Count > 0)
            .Select(e => new
            {
                field = e.Key,
                error = e.Value!.Errors[0].ErrorMessage
            });

        var response = new ApiResponse<object>("Invalid request payload", transactionId, errors);
        return new BadRequestObjectResult(response);
    };
});

builder.Services
        .AddBLL()
        .AddDAL(builder.Configuration);

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseMiddleware<TransactionIdMiddleware>(); // to add transactionid to each request
app.UseMiddleware<ErrorHandlingMiddleware>(); // to handle exceptions

app.MapControllers();

app.Run();
