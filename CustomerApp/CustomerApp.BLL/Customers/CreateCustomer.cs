using CustomerApp.DAL.Data;
using CustomerApp.DAL.Entities;

namespace CustomerApp.BLL.Customers;

public class CreateCustomer
{
    private readonly IUnitOfWork _unitOfWork;

    public CreateCustomer(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Customer> ExecuteAsync(string code, string name, string address)
    {
        var newCustomer = new Customer
        {
            Customercode = code,
            Customername = name,
            Customeraddress = address,
            Createdby = 0,
            Createdat = DateTime.Now,
            Modifiedby = 0,
            Modifiedat = DateTime.Now
        };
        await _unitOfWork.Customers.AddAsync(newCustomer);
        await _unitOfWork.SaveChangesAsync();
        return newCustomer;
    }
}