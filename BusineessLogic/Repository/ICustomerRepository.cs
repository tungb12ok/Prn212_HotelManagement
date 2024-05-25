using DataAccess.Models;

namespace BusineessLogic.Repository;

public abstract class ICustomerRepository
{
    public abstract Customer login(string email, string password);
    
}