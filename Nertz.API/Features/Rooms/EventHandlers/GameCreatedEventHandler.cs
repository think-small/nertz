using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Games;
using Nertz.API.Shared.Events;

namespace Nertz.API.Features.Rooms;

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