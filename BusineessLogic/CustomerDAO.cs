using DataAccess.Models;

namespace BusineessLogic;

public class CustomerDAO
{
    private static CustomerDAO _instance;
    private readonly FUMiniHotelManagementContext _context;
    private CustomerDAO()
    {
        _context = new FUMiniHotelManagementContext();
    }

    public static CustomerDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new CustomerDAO();
            }
            return _instance;
        }
    }

    public Customer login(string email, string pass)
    {
        return _context.Customers.FirstOrDefault(s => s.EmailAddress.Equals(email) && pass.Equals(s.Password));
    }
    public void AddCustomer(Customer customer)
    {
    }

    public void UpdateCustomer(Customer customer)
    {
    }

    public void DeleteCustomer(int customerId)
    {
    }

    public Customer GetCustomerById(int customerId)
    {
        return _context.Customers.FirstOrDefault(x => x.CustomerId == customerId);
    }

    public List<Customer> GetAllCustomers()
    {
        return new List<Customer>(); 
    }

    public List<Customer> SearchCustomers(string searchTerm)
    {
        return new List<Customer>(); 
    }
}