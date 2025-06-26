namespace CustomerApp.Api.Contracts;

public record UpdateCustomerRequest(int id, string? code, string? name, string? address);