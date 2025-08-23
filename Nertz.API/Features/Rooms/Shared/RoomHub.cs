using Microsoft.AspNetCore.SignalR;
using Nertz.API.Shared.Interfaces;
using Nertz.API.Shared.ViewModels;

namespace Nertz.API.Features.Rooms.Shared;

public class RoomHub : Hub<IRoomClient>
{
   public async Task SendOpenRooms(IEnumerable<RoomListItemVM> rooms)
   {
      await Clients.All.GetOpenRooms(rooms);
   }

   public async Task SendPlayers(IEnumerable<PlayerVM> players, int roomId)
   {
      await Clients.Group(GetGroupName(roomId)).GetPlayers(players);
   }
   
   public async Task JoinRoom(int roomId)
   {
      await Groups.AddToGroupAsync(Context.ConnectionId, GetGroupName(roomId));
   }
   
   public async Task LeaveRoom(int roomId)
   {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, GetGroupName(roomId));
   }
   
   public async Task StartGame(int roomId, int gameId)
   {
      await Clients.Group(GetGroupName(roomId)).GameStarted(gameId);
   }

   public static string GetGroupName(int roomId)
   {
      return $"room-{roomId}";
   }
}