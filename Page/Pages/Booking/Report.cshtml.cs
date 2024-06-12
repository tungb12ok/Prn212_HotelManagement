using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WindowsPresentation.LoginViewModel;

namespace Page.Pages.Booking
{
    public class ReportModel : PageModel
    {
        private readonly FUMiniHotelManagementContext _context;

        public ReportModel(FUMiniHotelManagementContext context)
        {
            _context = context;
        }

        [BindProperty] public DateTime? StartDate { get; set; }
        [BindProperty] public DateTime? EndDate { get; set; }
        public List<BookingHistoryViewModel> ReportData { get; set; }

        public void OnPost()
        {
            if (StartDate == null || EndDate == null)
            {
                ModelState.AddModelError("", "Please select both start and end dates.");
                return;
            }

            ReportData = _context.BookingReservations
                .Include(x => x.Customer)
                .Where(b => b.BookingDate >= StartDate && b.BookingDate <= EndDate)
                .OrderByDescending(b => b.BookingDate)
                .Select(b => new BookingHistoryViewModel
                {
                    BookingReservationId = b.BookingReservationId,
                    BookingDate = b.BookingDate,
                    TotalPrice = b.TotalPrice,
                    BookingStatus = b.BookingStatus,
                    CustomerName = b.Customer.CustomerFullName,
                    Rooms = b.BookingDetails.Select(d => new RoomDetailViewModel
                    {
                        RoomNumber = d.Room.RoomNumber,
                        StartDate = d.StartDate,
                        EndDate = d.EndDate,
                        ActualPrice = d.ActualPrice
                    }).ToList()
                }).ToList();
        }
    }

}