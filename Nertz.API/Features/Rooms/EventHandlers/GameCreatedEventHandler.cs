using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Games;
using Nertz.API.Features.Rooms.Shared;
using Nertz.API.Shared.Events;

namespace Nertz.API.Features.Rooms;

public class GameCreatedEventHandler : IEventHandler<GameCreatedEvent>
{
    private readonly IHubContext<GameHub> _hubContext;

    public GameCreatedEventHandler(IHubContext<GameHub> hubContext)
    {
        _hubContext = hubContext;
    }
    
    public async Task HandleAsync(GameCreatedEvent eventModel, CancellationToken cancelToken = default)
    {
        await _hubContext.Clients.Group(RoomHub.GetGroupName(eventModel.RoomId)).SendAsync(
            nameof(RoomHub.StartGame),
            eventModel.GameId,
            CancellationToken.None);
    }
}