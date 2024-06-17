using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

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

    public async Task<bool> ProcessBookingAsync(int[] roomIds, DateTime startDate, DateTime endDate, int customerId)
    {
        try
        {
            var rooms = await _context.RoomInformations
                .Where(r => roomIds.Contains(r.RoomId))
                .ToListAsync();

            int days = (endDate - startDate).Days;
            decimal totalPrice = rooms.Sum(r => r.RoomPricePerDay ?? 0 * days);

            var bookingReservation = new BookingReservation()
            {
                CustomerId = customerId,
                BookingDate = DateTime.Now,
                BookingStatus = 1,
                TotalPrice = totalPrice
            };

            _context.BookingReservations.Add(bookingReservation);
            await _context.SaveChangesAsync();

            foreach (var room in rooms)
            {
                var bookingDetail = new BookingDetail()
                {
                    BookingReservationId = bookingReservation.BookingReservationId,
                    RoomId = room.RoomId,
                    StartDate = startDate,
                    EndDate = endDate,
                    ActualPrice = room.RoomPricePerDay * days
                };

                _context.BookingDetails.Add(bookingDetail);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            // Handle exceptions (e.g., logging)
            return false;
        }
    }

    public async Task<List<RoomInformation>> GetAvailableRoomsAsync()
    {
        return await _context.RoomInformations
            .Where(r => r.RoomStatus == 1) // Assuming 1 means available
            .ToListAsync();
    }
    public async Task UpdateRoomStatusesAsync()
    {
        var currentDate = DateTime.Now;
        var roomsToUpdate = await _context.BookingDetails
            .Where(bd => bd.EndDate <= currentDate && bd.Room.RoomStatus != 1) // Assuming 1 means available
            .Select(bd => bd.Room)
            .Distinct()
            .ToListAsync();

        foreach (var room in roomsToUpdate)
        {
            room.RoomStatus = 1; // Set room to 'available'
        }

        await _context.SaveChangesAsync();
    }
}