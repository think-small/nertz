using ErrorOr;

namespace Nertz.Infrastructure.Contracts;

public interface IRoomRepository
{
    Task<ErrorOr<int>> CreateRoom(string name, int hostId, int maxPlayerCount, int targetScore, DateTimeOffset createdAt, CancellationToken cancelToken);
    Task<ErrorOr<bool>> JoinRoom(int roomId, int playerId, CancellationToken cancelToken);
    Task<ErrorOr<bool>> LeaveRoom(int roomId, int playerId, CancellationToken cancelToken);
    Task<ErrorOr<bool>> DeleteRoom(int roomId, CancellationToken cancelToken);
}