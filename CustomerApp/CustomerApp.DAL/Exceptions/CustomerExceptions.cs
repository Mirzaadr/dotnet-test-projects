namespace CustomerApp.DAL.Exceptions;

public class CustomerNotFoundException : AppException
{
    public CustomerNotFoundException(int id) : base($"customer with id {id.ToString()} was not found.") { }
}