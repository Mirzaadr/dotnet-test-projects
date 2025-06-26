using CustomerApp.DAL.Data;
using CustomerApp.DAL.Entities;
using CustomerApp.DAL.Exceptions;
using Microsoft.EntityFrameworkCore;
// using Customer.DAL;

namespace CustomerApp.BLL.Customers;

public class UpdateCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public UpdateCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer> ExecuteAsync(int customerId, string? code, string? name, string? address)
    {
        var currentCustomer = await _unitOfWork.Customers.FirstOrDefaultAsync(c => c.Customerid == customerId);
        if (currentCustomer is null)
        {
            throw new CustomerNotFoundException(customerId);
        }

        currentCustomer.Customercode = code ?? currentCustomer.Customercode;
        currentCustomer.Customername = name ?? currentCustomer.Customername;
        currentCustomer.Customeraddress = address ?? currentCustomer.Customeraddress;

        currentCustomer.Modifiedat = DateTime.Now;

        _unitOfWork.Customers.Update(currentCustomer);
        await _unitOfWork.SaveChangesAsync();

        return currentCustomer;
    }
}