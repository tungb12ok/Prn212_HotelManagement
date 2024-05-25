using DataAccess.Models;

namespace BusineessLogic.Repository;

public interface IRoomRepository
{
    void AddRoom(RoomInformation room);
    void UpdateRoom(RoomInformation room);
    void DeleteRoom(int roomId);
    RoomInformation GetRoomById(int roomId);
    List<RoomInformation> GetAllRooms();
    List<RoomInformation> SearchRooms(string searchTerm);
}