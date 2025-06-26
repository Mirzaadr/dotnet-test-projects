using CustomerApp.BLL.Customers;
using CustomerApp.DAL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApp.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBLL(
        this IServiceCollection services
    )
    {
        services.AddScoped<CreateCustomer>();
        services.AddScoped<GetAllCustomers>();
        services.AddScoped<GetCustomerById>();
        services.AddScoped<UpdateCustomer>();
        services.AddScoped<DeleteCustomer>();
        return services;
    }
}
