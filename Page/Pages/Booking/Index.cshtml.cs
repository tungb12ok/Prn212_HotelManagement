using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Page.ViewModel;

namespace Page.Pages.Booking
{
    public class BookingModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

        public BookingModel(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty] public DateTime StartDate { get; set; }

        [BindProperty] public DateTime EndDate { get; set; }
        [BindProperty] public int customerId { get; set; }
        public List<SelectListItem> Rooms { get; set; }

        public void OnGet()
        {
            Rooms = _context
                .RoomInformations
                .Include(x => x.RoomType)
                .Where(s => s.RoomStatus == 1)
                .Select(r => new SelectListItem
                {
                    Value = r.RoomId.ToString(),
                    Text =
                        $"{r.RoomNumber} Room Type: {r.RoomType.RoomTypeName}. Max: {r.RoomMaxCapacity}. Prices {r.RoomPricePerDay}"
                })
                .ToList();
        }

        public async Task<IActionResult> OnPostAsync(int []RoomIds)
        {
            decimal totalPrice = 0;
            // Retrieve all the rooms at once based on the provided RoomIds
            var rooms = _context.RoomInformations
                .Where(r => RoomIds.Contains(r.RoomId))
                .ToList();

            // Calculate total price
            int days = (EndDate - StartDate).Days;
            foreach (var room in rooms)
            {
                totalPrice += room.RoomPricePerDay ?? 0 * days;
            }

            // Create a booking reservation
            var bookingReservation = new BookingReservation()
            {
                CustomerId = customerId,
                BookingDate = DateTime.Now,
                BookingStatus = 1,
                TotalPrice = totalPrice
            };

            // Add and save booking reservation to get the ID
            _context.BookingReservations.Add(bookingReservation);
            await _context.SaveChangesAsync();

            // Create booking details for each room
            foreach (var room in rooms)
            {
                var bookingDetail = new BookingDetail()
                {
                    BookingReservationId = bookingReservation.BookingReservationId,
                    RoomId = room.RoomId,
                    StartDate = StartDate,
                    EndDate = EndDate,
                    ActualPrice = room.RoomPricePerDay * days
                };
                _context.BookingDetails.Add(bookingDetail);
            }

            // Commit all booking details at once
            await _context.SaveChangesAsync();

            return RedirectToPage("./BookingConfirmation");
        }
    }
}