using Microsoft.EntityFrameworkCore;
using OrderService.Domain.OrderAggregate;

namespace OrderService.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Order> Orders => Set<Order>();

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(x => x.Id);

            entity.Property(x => x.CustomerEmail)
                  .IsRequired();

            entity.Property(x => x.TotalAmount)
                  .HasColumnType("decimal(18,2)");

            entity.Property(x => x.Status)
                  .IsRequired();
        });
    }
}