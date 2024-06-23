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
    Task<bool> ProcessBookingAsync(int[] roomIds, DateTime startDate, DateTime endDate, int customerId);
    Task<List<RoomInformation>> GetAvailableRoomsAsync();
    Task UpdateRoomStatusesAsync();
    Task<decimal> CalculateTotalPriceAsync(int[] roomIds, DateTime startDate, DateTime endDate);
    Task<int> CreateBookingReservationAsync(BookingReservation bookingReservation);
    Task CreateBookingDetailAsync(int bookingReservationId, int roomId, DateTime startDate, DateTime endDate, decimal actualPrice);
    Task<decimal> ActualTotalPriceAsync(int[] roomIds, DateTime startDate, DateTime endDate);

}