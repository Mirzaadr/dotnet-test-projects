using CustomerApp.DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CustomerApp.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDAL(
        this IServiceCollection services,
        IConfigurationManager configuration
    )
    {
        services.AddDbContext<AppDbContext>(
            options => options.UseNpgsql(configuration.GetConnectionString("Default"))
        );

        services.AddScoped<IUnitOfWork>(sp =>
            sp.GetRequiredService<AppDbContext>()
        );
        return services;
    }
}
