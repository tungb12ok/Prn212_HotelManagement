using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusineessLogic.Repository;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Page.EmailServices;
using Page.Ultils;
using Page.ViewModel;

namespace Page.Pages.Booking
{
    public class BookingModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;
        private readonly IHubContext<BookingHub> _hubContext;
        private readonly IEmailService _emailService;
        private readonly IBookingRepository _bookingRepository;

        public BookingModel(FUMiniHotelManagementContext context, IHubContext<BookingHub> hubContext,
            IEmailService emailService, IBookingRepository bookingRepository)
        {
            _context = context;
            _hubContext = hubContext;
            _emailService = emailService;
            _bookingRepository = bookingRepository;
        }

        [BindProperty]
        public DateTime StartDate { get; set; }

        [BindProperty]
        public DateTime EndDate { get; set; }

        [BindProperty]
        public int CustomerId { get; set; } // Corrected property name to follow C# naming conventions

        public List<SelectListItem> Rooms { get; set; }

        public IActionResult OnGet()
        {
            // Check if customer is logged in using session
            Customer checkLogin = HttpContext.Session.Get<Customer>("user");
            if (checkLogin == null)
            {
                return Redirect("~/Login");
            }

            CustomerId = checkLogin.CustomerId;
            // Load available rooms into SelectListItems
            Rooms = _context.RoomInformations
                .Include(x => x.RoomType)
                .Where(s => s.RoomStatus == 1)
                .Select(r => new SelectListItem
                {
                    Value = r.RoomId.ToString(),
                    Text = $"{r.RoomNumber} Room Type: {r.RoomType.RoomTypeName}. Max: {r.RoomMaxCapacity}. Price: {r.RoomPricePerDay}"
                })
                .ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int[] RoomIds)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                // Calculate total price for the booking
                decimal totalPrice = await _bookingRepository.ActualTotalPriceAsync(RoomIds, StartDate, EndDate);

                // Create a new booking reservation object
                var bookingReservation = new BookingReservation()
                {
                    CustomerId = CustomerId, // Set the customer ID
                    BookingDate = DateTime.Now, // Set the booking date
                    BookingStatus = 0, // Set the booking status (pending, for example)
                    TotalPrice = totalPrice // Set the total price
                };

                // Create the booking reservation and retrieve its ID
                int bookingReservationId = await _bookingRepository.CreateBookingReservationAsync(bookingReservation);

                // Create booking details for each room selected
                foreach (var roomId in RoomIds)
                {
                    // Calculate actual price for each room (if needed)
                    decimal actualPrice = await _bookingRepository.ActualTotalPriceAsync(new int[] { roomId }, StartDate, EndDate);

                    // Create booking detail
                    await _bookingRepository.CreateBookingDetailAsync(bookingReservationId, roomId, StartDate, EndDate, actualPrice);

                    // Send email confirmation to customer
                    Customer customer = await _context.Customers.FindAsync(CustomerId);
                    string confirmationLink = $"{Request.Scheme}://{Request.Host}/Booking/ConfirmEmail?id={bookingReservationId}&total={totalPrice}";
                    await _emailService.SendEmailAsync(customer.EmailAddress, "Confirm booking hotel", confirmationLink);
                }

                return RedirectToPage("/Booking/BookingHistory");
            }
            catch (Exception ex)
            {
                // Handle exception
                return BadRequest($"Failed to create booking: {ex.Message}");
            }
        }
    }
}
