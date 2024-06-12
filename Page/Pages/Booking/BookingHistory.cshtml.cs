using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WindowsPresentation.LoginViewModel;

namespace Page.Pages.Booking
{
    public class BookingHistoryModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

        public BookingHistoryModel(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        public List<BookingHistoryViewModel> BookingHistory { get; set; }

        public void OnGet(int customerId)
        {
            BookingHistory = _context.BookingReservations
                .Where(b => b.CustomerId == customerId)
                .Select(b => new BookingHistoryViewModel
                {
                    BookingReservationId = b.BookingReservationId,
                    BookingDate = b.BookingDate,
                    TotalPrice = b.TotalPrice,
                    BookingStatus = b.BookingStatus,
                    Rooms = b.BookingDetails.Select(d => new RoomDetailViewModel
                    {
                        RoomNumber = d.Room.RoomNumber,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        ActualPrice = d.ActualPrice
                    }).ToList()
                })
                .ToList();
        }
    }
}
