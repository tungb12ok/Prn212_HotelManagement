using System.Linq;
using System.Windows;
using DataAccess.Models;

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
            .Select(b => new
            {
                b.BookingReservationId,
                b.BookingDate,
                b.TotalPrice,
                b.BookingStatus,
                Rooms = b.BookingDetails.Select(d => new
                {
                    d.Room.RoomNumber,
                    d.StartDate,
                    d.EndDate,
                    d.ActualPrice
                })
            })
            .ToList();

        BookingHistoryDataGrid.ItemsSource = bookingHistory;
    }
}