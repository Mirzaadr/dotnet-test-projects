using Microsoft.EntityFrameworkCore;
using CustomerApp.DAL.Entities;

namespace CustomerApp.DAL.Data;

public interface IUnitOfWork
{
    DbSet<Customer> Customers { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}