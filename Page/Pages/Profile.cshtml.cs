using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Page.Ultils;

namespace Page.Pages
{
    public class ProfileModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

        public ProfileModel(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty] public Customer Customer { get; set; } = null;

        public void OnGet()
        {
            Customer = HttpContext.Session.Get<Customer>("user");
            if (Customer == null)
            {
                RedirectToPage("/NotFound");
            }
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var customerInDb = _context.Customers.FirstOrDefault(c => c.CustomerId == Customer.CustomerId);
            if (customerInDb == null)
            {
                RedirectToPage("/NotFound");
            }

            customerInDb.CustomerFullName = Customer.CustomerFullName;
            customerInDb.Telephone = Customer.Telephone;
            customerInDb.EmailAddress = Customer.EmailAddress;
            customerInDb.Password = Customer.Password;  // Consider encrypting the password or using hash.
            customerInDb.CustomerBirthday = Customer.CustomerBirthday;

            _context.SaveChanges();

            TempData["Message"] = "Profile updated successfully.";
            return Redirect("Profile?customerId="+Customer.CustomerId);
        }
    }
}
