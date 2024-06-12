using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Page.Ultils;

namespace Page.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LogoutModel(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public IActionResult OnGet()
        {
            _httpContextAccessor.HttpContext.Session.Set<Customer>("user", null);
            return Redirect("/Index"); 
        }
    }
}
