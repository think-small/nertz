using FastEndpoints;
using ErrorOr;

namespace Nertz.API.Features.Rooms;

public class LeaveRoomCommand : ICommand<ErrorOr<LeaveRoomResponse>>
{
    public int PlayerId { get; init; }
    public int RoomId { get; init; }
}