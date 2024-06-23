using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace BusineessLogic.Repository;

public class BookingRepository : IBookingRepository
{
    public BookingRepository(FUMiniHotelManagementContext context)
    {
        _context = context;
    }

    private readonly FUMiniHotelManagementContext _context;

  

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
            .Include(xx => xx.BookingReservation)
            .Where(bd => bd.EndDate <= currentDate && bd.Room.RoomStatus != 1 && bd.BookingReservation.BookingStatus == 1) // Assuming 1 means available
            .Select(bd => bd.Room)
            .Distinct()
            .ToListAsync();

        foreach (var room in roomsToUpdate)
        {
            room.RoomStatus = 1; // Set room to 'available'
        }

        await _context.SaveChangesAsync();
    }
    public async Task<decimal> CalculateTotalPriceAsync(int[] roomIds, DateTime startDate, DateTime endDate)
    {
        decimal totalPrice = 0;

        var rooms = await _context.RoomInformations
            .Where(r => roomIds.Contains(r.RoomId))
            .ToListAsync();

        int days = (endDate - startDate).Days;

        foreach (var room in rooms)
        {
            totalPrice += (room.RoomPricePerDay ?? 0) * days;
        }

        return totalPrice;
    }
    public async Task<int> CreateBookingReservationAsync(BookingReservation bookingReservation)
    {
        _context.BookingReservations.Add(bookingReservation);
        await _context.SaveChangesAsync();
        
        return bookingReservation.BookingReservationId; // Return the ID of the newly created booking reservation
    }
    public async Task<int> CreateBookingReservationAsync(int customerId, decimal totalPrice)
    {
        var bookingReservation = new BookingReservation()
        {
            CustomerId = customerId,
            BookingDate = DateTime.Now,
            BookingStatus = 1,
            TotalPrice = totalPrice
        };

        _context.BookingReservations.Add(bookingReservation);
        await _context.SaveChangesAsync();

        return bookingReservation.BookingReservationId;
    }

    public async Task CreateBookingDetailAsync(int bookingReservationId, int roomId, DateTime startDate, DateTime endDate, decimal actualPrice)
    {
        var room = await _context.RoomInformations.FindAsync(roomId);
        if (room != null)
        {
            room.RoomStatus = 0;
            var bookingDetail = new BookingDetail()
            {
                BookingReservationId = bookingReservationId,
                RoomId = roomId,
                StartDate = startDate,
                EndDate = endDate,
                ActualPrice = actualPrice
            };

            _context.BookingDetails.Add(bookingDetail);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<decimal> ActualTotalPriceAsync(int[] roomIds, DateTime startDate, DateTime endDate)
    {
        decimal totalPrice = 0;

        var rooms = await _context.RoomInformations
            .Where(r => roomIds.Contains(r.RoomId))
            .ToListAsync();

        int days = (endDate - startDate).Days;

        foreach (var room in rooms)
        {
            totalPrice += (room.RoomPricePerDay ?? 0) * days;
        }

        return totalPrice;
    }
}