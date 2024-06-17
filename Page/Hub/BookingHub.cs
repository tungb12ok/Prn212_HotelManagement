using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

public class BookingHub : Hub
{
    public async Task UpdateRoomAvailability(int[] roomIds)
    {
        await Clients.All.SendAsync("ReceiveRoomUpdate", roomIds);
    }
}