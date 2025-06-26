using CustomerApp.DAL.Data;
using CustomerApp.DAL.Entities;
using Microsoft.EntityFrameworkCore;
// using Customer.DAL;

namespace CustomerApp.BLL.Customers;

public class GetAllCustomers
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllCustomers(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Customer>> ExecuteAsync()
    {
        return await _unitOfWork.Customers.AsNoTracking().ToListAsync();
    }
}