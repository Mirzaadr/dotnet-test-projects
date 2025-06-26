using System.ComponentModel.DataAnnotations;
using System.Net;
using CustomerApp.Api.Contracts;
using CustomerApp.DAL.Exceptions;

namespace CustomerApp.Api.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        string transactionId = context.Items["TransactionId"]?.ToString() ?? Guid.NewGuid().ToString();

        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            context.Response.StatusCode = 400;
            _logger.LogWarning(ex, "Handled app exception");

            await WriteJsonResponse(context, ex.Message, transactionId, 400);
        }
        catch (Exception ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            _logger.LogError(ex, "Unhandled exception");

            await WriteJsonResponse(context, "An unexpected error occurred", transactionId, 500);
        }
    }

    private async Task WriteJsonResponse(HttpContext context, string message, string transactionId, int statusCode)
    {
        context.Response.ContentType = "application/json";

        var response = new ApiResponse<object?>(message, transactionId, null);
        var json = System.Text.Json.JsonSerializer.Serialize(response);

        await context.Response.WriteAsync(json);
    }
}
