using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.Application.Nertz.Features.Games;
using Nertz.Application.Shared.Events;

namespace Nertz.Application.Nertz.Features.Rooms;

public class GameCreatedEventHandler : IEventHandler<GameCreatedEvent>
{
    private readonly IHubContext<GameHub> _hubContext;

    public GameCreatedEventHandler(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public Task HandleAsync(GameCreatedEvent eventModel, CancellationToken cancelToken = default)
    {
        Console.WriteLine(eventModel);
        return Task.CompletedTask;
    }
}