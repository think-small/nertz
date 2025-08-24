using FastEndpoints;

namespace Nertz.API.Features.Rooms.Shared;

public class RoomPlayersChangedEvent : IEvent
{
    public int RoomId { get; init; }
}