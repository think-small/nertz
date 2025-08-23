using FastEndpoints;

namespace Nertz.API.Features.Rooms.Shared;

public class JoinedRoomEvent : IEvent
{
    public int RoomId { get; init; }
}