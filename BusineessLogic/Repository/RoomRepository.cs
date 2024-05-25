using DataAccess.Models;

namespace BusineessLogic.Repository;

public class RoomRepository : IRoomRepository
{
    private readonly FUMiniHotelManagementContext _context;

    public RoomRepository()
    {
        _context = new FUMiniHotelManagementContext();
    }

    public void AddRoom(RoomInformation room)
    {
        _context.RoomInformations.Add(room);
        _context.SaveChanges();
    }

    public void UpdateRoom(RoomInformation room)
    {
        _context.RoomInformations.Update(room);
        _context.SaveChanges();
    }

    public void DeleteRoom(int roomId)
    {
        var room = _context.RoomInformations.Find(roomId);
        if (room != null)
        {
            _context.RoomInformations.Remove(room);
            _context.SaveChanges();
        }
    }

    public RoomInformation GetRoomById(int roomId)
    {
        return _context.RoomInformations.Find(roomId);
    }

    public List<RoomInformation> GetAllRooms()
    {
        return _context.RoomInformations.ToList();
    }

    public List<RoomInformation> SearchRooms(string searchTerm)
    {
        return _context.RoomInformations
            .Where(r => r.RoomNumber.Contains(searchTerm) || r.RoomDetailDescription.Contains(searchTerm))
            .ToList();
    }
}