using Microsoft.AspNetCore.SignalR;
using Nertz.Application.Shared.Interfaces;

namespace Nertz.Application.Nertz.Features.Rooms;

public class RoomHub : Hub<IRoomHub>
{
   public async Task JoinRoom(int roomId)
   {
      await Groups.AddToGroupAsync(Context.ConnectionId, $"room-{roomId}");
   }
   
   public async Task LeaveRoom(int roomId)
   {
      await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"room-{roomId}");
   }
   
   public async Task GameStarted(int roomId, int gameId)
   {
      await Clients.Group($"room-{roomId}").GameStarted(gameId);
   } 
}