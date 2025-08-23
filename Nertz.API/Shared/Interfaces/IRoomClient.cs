using Nertz.API.Features.Rooms.Shared;
using Nertz.API.Shared.ViewModels;

namespace Nertz.API.Shared.Interfaces;

public interface IRoomClient
{
    Task GetPlayers(IEnumerable<PlayerVM> players);
    Task GetOpenRooms(IEnumerable<Room> rooms);
    Task JoinRoom(int roomId);
    Task LeaveRoom(int roomId);
    Task GameStarted(int gameId);
}