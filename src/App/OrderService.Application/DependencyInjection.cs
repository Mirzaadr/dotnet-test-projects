using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Orders.Command.CreateOrder;

namespace OrderService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // services.AddMediatR(typeof(DependencyInjection).Assembly);
        services.AddScoped<CreateOrderCommandHandler>();
    
        return services;
    }
}
