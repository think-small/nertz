using FastEndpoints;
using Nertz.Application.Nertz;

namespace Nertz.Application.Shared.Events;

public class GameCreatedEvent : IEvent
{
    public required Game Game { get; init; }
}