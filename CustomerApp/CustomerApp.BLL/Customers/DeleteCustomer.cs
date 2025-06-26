using CustomerApp.DAL.Data;
using CustomerApp.DAL.Entities;
using CustomerApp.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
// using Customer.DAL;

namespace CustomerApp.BLL.Customers;

public class DeleteCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task ExecuteAsync(int customerId)
    {
        var customer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Customerid == customerId);
        if (customer is null)
        {
            throw new CustomerNotFoundException(customerId);
        }

        _unitOfWork.Customers.Remove(customer);
        await _unitOfWork.SaveChangesAsync();
    }
}