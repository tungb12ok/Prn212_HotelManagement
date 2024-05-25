using DataAccess.Models;

namespace BusineessLogic.Repository;

public class RoomDAO
{
    private static RoomDAO _instance;
    private readonly IRoomRepository _roomRepository;

    private RoomDAO()
    {
        _roomRepository = new RoomRepository();
    }

    public static RoomDAO Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new RoomDAO();
            }
            return _instance;
        }
    }

    public void AddRoom(RoomInformation room)
    {
        _roomRepository.AddRoom(room);
    }

    public void UpdateRoom(RoomInformation room)
    {
        _roomRepository.UpdateRoom(room);
    }

    public void DeleteRoom(int roomId)
    {
        _roomRepository.DeleteRoom(roomId);
    }

    public RoomInformation GetRoomById(int roomId)
    {
        return _roomRepository.GetRoomById(roomId);
    }

    public List<RoomInformation> GetAllRooms()
    {
        return _roomRepository.GetAllRooms();
    }

    public List<RoomInformation> SearchRooms(string searchTerm)
    {
        return _roomRepository.SearchRooms(searchTerm);
    }
}