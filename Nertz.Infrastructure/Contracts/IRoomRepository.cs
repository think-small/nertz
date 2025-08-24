using ErrorOr;
using Nertz.Infrastructure.DataModels;

namespace Nertz.Infrastructure.Contracts;

public interface IRoomRepository
{
    Task<ErrorOr<IEnumerable<RoomListItemDataModel>>> GetRooms(bool shouldGetOnlyOpenRooms, CancellationToken cancelToken);
    Task<ErrorOr<RoomListItemDataModel>> GetRoom(int roomId, CancellationToken cancelToken);
    Task<ErrorOr<bool>> AssignHost(int roomId, int hostId, CancellationToken cancelToken);
    Task<ErrorOr<int>> CreateRoom(string name, int hostId, int maxPlayerCount, int targetScore, DateTimeOffset createdAt, CancellationToken cancelToken);
    Task<ErrorOr<bool>> JoinRoom(int roomId, int playerId, CancellationToken cancelToken);
    Task<ErrorOr<bool>> LeaveRoom(int roomId, int playerId, CancellationToken cancelToken);
    Task<ErrorOr<bool>> MarkRoomForDeletion(int roomId, DateTimeOffset markForDeletionAt, CancellationToken cancelToken);
    Task<ErrorOr<bool>> DeleteRoom(int roomId, CancellationToken cancelToken);
    Task<ErrorOr<IEnumerable<PlayerDataModel>>> GetRoomPlayers(int roomId, CancellationToken cancelToken);
}