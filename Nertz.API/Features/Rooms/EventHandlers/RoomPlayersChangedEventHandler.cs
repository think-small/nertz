using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Rooms.Shared;
using Nertz.API.Shared.ViewModels;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class RoomPlayersChangedEventHandler : IEventHandler<RoomPlayersChangedEvent>
{
    private readonly IRoomRepository _repository;
    private readonly IHubContext<RoomHub> _hubContext;

    public RoomPlayersChangedEventHandler(IRoomRepository repository, IHubContext<RoomHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    public async Task HandleAsync(RoomPlayersChangedEvent playersChangedEventModel, CancellationToken cancelToken)
    {
        var playersResult = await _repository.GetRoomPlayers(playersChangedEventModel.RoomId, cancelToken);

        if (playersResult.IsError)
        {
            throw new NotImplementedException();
        }

        var players = playersResult
            .Value
            .Select(p => new PlayerVM { UserName = p.UserName, IsHost = p.IsHost });
        
        await _hubContext.Clients
            .Group(RoomHub.GetGroupName(playersChangedEventModel.RoomId))
            .SendAsync(nameof(RoomHub.SendPlayers), players, CancellationToken.None);
    }
}