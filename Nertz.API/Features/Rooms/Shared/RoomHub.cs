using Microsoft.AspNetCore.SignalR;
using Nertz.API.Shared.Interfaces;

namespace Nertz.API.Features.Rooms.Shared;

public class RoomHub : Hub<IRoomClient>
{
   public async Task SendOpenRooms(IEnumerable<Room> rooms)
   {
      await Clients.All.GetOpenRooms(rooms);
   }
   
   public async Task JoinRoom(int roomId)
   {
      await Groups.AddToGroupAsync(Context.ConnectionId, GetRoomHubGroupName(roomId));
   }
   
   public async Task LeaveRoom(int roomId)
   {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetRoomHubGroupName(roomId));
   }
   
   public async Task StartGame(int roomId, int gameId)
   {
      await Clients.Group(GetRoomHubGroupName(roomId)).GameStarted(gameId);
   }

   public static string GetRoomHubGroupName(int roomId)
   {
      return $"room-{roomId}";
   }
}