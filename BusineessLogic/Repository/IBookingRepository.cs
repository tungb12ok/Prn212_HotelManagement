using DataAccess.Models;

namespace BusineessLogic.Repository;

public interface IBookingRepository
{
    void AddBooking(BookingReservation booking);
    void UpdateBooking(BookingReservation booking);
    void DeleteBooking(int bookingId);
    BookingReservation GetBookingById(int bookingId);
    List<BookingReservation> GetAllBookings();
    List<BookingReservation> GetBookingsByCustomerId(int customerId);
}