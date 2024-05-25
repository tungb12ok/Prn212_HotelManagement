using DataAccess.Models;

namespace BusineessLogic.Repository;

public class BookingRepository : IBookingRepository
{
    private readonly FUMiniHotelManagementContext _context;

    public BookingRepository()
    {
        _context = new FUMiniHotelManagementContext();
    }

    public void AddBooking(BookingReservation booking)
    {
        _context.BookingReservations.Add(booking);
        _context.SaveChanges();
    }

    public void UpdateBooking(BookingReservation booking)
    {
        _context.BookingReservations.Update(booking);
        _context.SaveChanges();
    }

    public void DeleteBooking(int bookingId)
    {
        var booking = _context.BookingReservations.Find(bookingId);
        if (booking != null)
        {
            _context.BookingReservations.Remove(booking);
            _context.SaveChanges();
        }
    }

    public BookingReservation GetBookingById(int bookingId)
    {
        return _context.BookingReservations.Find(bookingId);
    }

    public List<BookingReservation> GetAllBookings()
    {
        return _context.BookingReservations.ToList();
    }

    public List<BookingReservation> GetBookingsByCustomerId(int customerId)
    {
        return _context.BookingReservations
            .Where(b => b.CustomerId == customerId)
            .ToList();
    }
}