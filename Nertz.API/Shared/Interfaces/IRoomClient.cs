using Nertz.API.Features.Rooms.Shared;

namespace Nertz.API.Shared.Interfaces;

public interface IRoomClient
{
    Task GetOpenRooms(IEnumerable<Room> rooms);
    Task JoinRoom(int roomId);
    Task LeaveRoom(int roomId);
    Task GameStarted(int gameId);
}