using CustomerApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CustomerApp.DAL.Data;

public class AppDbContext : DbContext, IUnitOfWork
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    { }

    public virtual DbSet<Customer> Customers { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
          .ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}