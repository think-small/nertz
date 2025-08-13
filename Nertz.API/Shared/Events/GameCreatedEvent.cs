using FastEndpoints;
using Nertz.API.Features.Games.Shared;

namespace Nertz.API.Shared.Events;

public class GameCreatedEvent : IEvent
{
    public required Game Game { get; init; }
    public int RoomId { get; init; }
}