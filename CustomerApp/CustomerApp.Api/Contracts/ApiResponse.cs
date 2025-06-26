namespace CustomerApp.Api.Contracts;

public class ApiResponse<T>
{
    public string Message { get; set; } = string.Empty;
    public string TransactionId { get; set; } = string.Empty;
    public T? Data { get; set; }

    public ApiResponse(string message, string transactionId, T? data = default)
    {
        Message = message;
        TransactionId = transactionId;
        Data = data;
    }
}