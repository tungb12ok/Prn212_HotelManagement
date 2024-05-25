using DataAccess.Models;

namespace BusineessLogic.Repository;

public class BookingDAO
{
    private static BookingDAO _instance;
    private readonly IBookingRepository _bookingRepository;

    private BookingDAO()
    {
        _bookingRepository = new BookingRepository();
    }

    public static BookingDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new BookingDAO();
            }
            return _instance;
        }
    }

    public void AddBooking(BookingReservation booking)
    {
        _bookingRepository.AddBooking(booking);
    }

    public void UpdateBooking(BookingReservation booking)
    {
        _bookingRepository.UpdateBooking(booking);
    }

    public void DeleteBooking(int bookingId)
    {
        _bookingRepository.DeleteBooking(bookingId);
    }

    public BookingReservation GetBookingById(int bookingId)
    {
        return _bookingRepository.GetBookingById(bookingId);
    }

    public List<BookingReservation> GetAllBookings()
    {
        return _bookingRepository.GetAllBookings();
    }

    public List<BookingReservation> GetBookingsByCustomerId(int customerId)
    {
        return _bookingRepository.GetBookingsByCustomerId(customerId);
    }
}