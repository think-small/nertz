using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class RoomListUpdatedEventHandler : IEventHandler<RoomListUpdatedEvent>
{
    private readonly IRoomRepository _repository;
    private readonly IHubContext<RoomHub> _hubContext;

    public RoomListUpdatedEventHandler(IRoomRepository repository, IHubContext<RoomHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }
    
    public async Task HandleAsync(RoomListUpdatedEvent eventModel, CancellationToken cancelToken)
    {
        var roomData = await _repository.GetRooms(true, cancelToken);

        if (roomData.IsError)
        {
            throw new NotImplementedException();
        }
        
        var rooms = roomData.Value.Select(RoomListItemVM.FromDataModel);
        await _hubContext.Clients.All.SendAsync(nameof(RoomHub.SendOpenRooms), rooms, CancellationToken.None);
    }
}