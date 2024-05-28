using DataAccess.Models;

namespace BusineessLogic.Repository;

public class CustomerRepository : ICustomerRepository
{
    public override Customer login(string email, string password) => CustomerDAO.Instance.login(email, password);
    public override Customer CustomerById(int id) => CustomerDAO.Instance.GetCustomerById(id);
}