using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Page.Pages.Booking;

public class ConfirmEmail : PageModel
{
    public ConfirmEmail(FUMiniHotelManagementContext context)
    {
        _context = context;
    }

    private readonly FUMiniHotelManagementContext _context;
    
    [BindProperty(SupportsGet = true)]
    public int BookingId { get; set; }

    [BindProperty(SupportsGet = true)]
    public decimal TotalPrice { get; set; }

    public async Task<IActionResult> OnGetAsync(int id, decimal total)
    {
        BookingId = id;
        TotalPrice = total;

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var booking = await _context.BookingReservations.FindAsync(BookingId);

        if (booking == null)
        {
            return NotFound();
        }

        // Perform actions to confirm the booking (update status, etc.)
        booking.BookingStatus = 1; // Example: Set status to confirmed
        _context.SaveChanges();

        return RedirectToPage("/Booking/BookingHistory"); // Redirect to booking history or another page
    }
}