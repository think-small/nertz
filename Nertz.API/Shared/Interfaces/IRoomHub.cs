namespace Nertz.API.Shared.Interfaces;

public interface IRoomHub
{
    Task JoinRoom(int roomId);
    Task LeaveRoom(int roomId);
    Task GameStarted(int gameId);
}