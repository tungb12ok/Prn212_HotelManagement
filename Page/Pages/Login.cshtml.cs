using DataAccess.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Policy;
using System.ComponentModel.DataAnnotations;
using Page.Ultils;

namespace Page.Pages
{
    public class LoginModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;
        private readonly IConfiguration _configuration;

        public LoginModel(FUMiniHotelManagementContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            var adminEmail = _configuration["AdminCredentials:Email"];
            var adminPassword = _configuration["AdminCredentials:Password"];
            Customer c = _context.Customers.FirstOrDefault(x => x.EmailAddress == Input.EmailAddress && x.Password == Input.Password);
            if (Input.EmailAddress == adminEmail && Input.Password == adminPassword)
            {
                HttpContext.Session.Set<Customer>("user", new Customer
                {
                    CustomerId = 0,
                    CustomerFullName = "admin"
                });
                return Redirect("Index");
            }
            else if (c != null)
            {
                HttpContext.Session.Set<Customer>("user", c);
                return Redirect("Index");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Page();
        }
       
        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string EmailAddress { get; set; }

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
        }
    }
}
