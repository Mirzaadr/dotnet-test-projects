namespace CustomerApp.Api.Middleware;

public class TransactionIdMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var transactionId = Guid.NewGuid().ToString();
        context.Items["TransactionId"] = transactionId;

        context.Response.Headers.Append("X-Transaction-Id", transactionId);

        await _next(context);
    }
}