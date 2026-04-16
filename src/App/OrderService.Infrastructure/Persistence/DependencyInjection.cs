using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Application.Repositories;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Infrastructure.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(
                config.GetConnectionString("Default")));

        services.AddScoped<IOrderRepository, OrderRepository>();

        return services;
    }
}