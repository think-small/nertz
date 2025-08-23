using FastEndpoints;
using ErrorOr;

namespace Nertz.Application.Features.Rooms;

public class JoinRoomCommand : ICommand<ErrorOr<JoinRoomResponse>>
{
    public int RoomId { get; init; }
    public int PlayerId { get; init; } // TODO - replace with getting playerId from auth token when auth is setup
}