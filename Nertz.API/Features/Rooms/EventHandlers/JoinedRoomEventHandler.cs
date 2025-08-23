using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Rooms.Shared;
using Nertz.API.Shared.ViewModels;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class JoinedRoomEventHandler : IEventHandler<JoinedRoomEvent>
{
    private readonly IRoomRepository _repository;
    private readonly IHubContext<RoomHub> _hubContext;

    public JoinedRoomEventHandler(IRoomRepository repository, IHubContext<RoomHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }

    public async Task HandleAsync(JoinedRoomEvent eventModel, CancellationToken cancelToken)
    {
        var playersResult = await _repository.GetRoomPlayers(eventModel.RoomId, cancelToken);

        if (playersResult.IsError)
        {
            throw new NotImplementedException();
        }

        var players = playersResult
            .Value
            .Select(p => new PlayerVM { UserName = p.UserName });
        
        await _hubContext.Clients
            .Group(RoomHub.GetGroupName(eventModel.RoomId))
            .SendAsync(nameof(RoomHub.SendPlayers), players, CancellationToken.None);
    }
}