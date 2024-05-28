using System.Linq;
using System.Windows;
using DataAccess.Models;
using WindowsPresentation.LoginViewModel;

namespace WindowsPresentation;

public partial class BookingHistoryWindow : Window
{
    private readonly FUMiniHotelManagementContext _context;

    public BookingHistoryWindow(int customerId)
    {
        InitializeComponent();
        _context = new FUMiniHotelManagementContext();
        LoadBookingHistory(customerId);
    }

    private void LoadBookingHistory(int customerId)
    {
        var bookingHistory = _context.BookingReservations
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

        BookingHistoryDataGrid.ItemsSource = bookingHistory;
    }
}