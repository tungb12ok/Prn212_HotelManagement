using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Page.Pages.ManageCustomers
{
    public class IndexModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

    public IndexModel(FUMiniHotelManagementContext context)
    {
        _context = context;
    }

    [BindProperty]
    public Customer NewCustomer { get; set; } = new Customer();

    [BindProperty]
    public Customer Customer { get; set; }

    public List<Customer> Customers { get; set; }

    public async Task OnGetAsync()
    {
        Customers = await _context.Customers.ToListAsync();
    }

    public async Task<IActionResult> OnPostAddCustomerAsync()
    {
        if (!ModelState.IsValid)
        {
            Customers = await _context.Customers.ToListAsync();
            return Page();
        }

        _context.Customers.Add(NewCustomer);
        await _context.SaveChangesAsync();
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostEditCustomerAsync()
    {
        if (!ModelState.IsValid)
        {
            Customers = await _context.Customers.ToListAsync();
            return Page();
        }

        var dbCustomer = await _context.Customers.FindAsync(Customer.CustomerId);
        if (dbCustomer != null)
        {
            dbCustomer.CustomerFullName = Customer.CustomerFullName;
            dbCustomer.Telephone = Customer.Telephone;
            dbCustomer.EmailAddress = Customer.EmailAddress;
            dbCustomer.Password = Customer.Password; // Ensure this is handled securely in a real application
            dbCustomer.CustomerBirthday = Customer.CustomerBirthday;
            dbCustomer.CustomerStatus = Customer.CustomerStatus;

            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }

    public async Task<IActionResult> OnPostDeleteCustomerAsync(int customerId)
    {
        var customer = await _context.Customers.FindAsync(customerId);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }
        return RedirectToPage();
    }
    }
}
