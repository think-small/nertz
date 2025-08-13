using FastEndpoints;
using Microsoft.AspNetCore.SignalR;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class RoomCreatedEventHandler : IEventHandler<RoomCreatedEvent>
{
    private readonly IRoomRepository _repository;
    private readonly IHubContext<RoomHub> _hubContext;

    public RoomCreatedEventHandler(IRoomRepository repository, IHubContext<RoomHub> hubContext)
    {
        _repository = repository;
        _hubContext = hubContext;
    }
    
    public async Task HandleAsync(RoomCreatedEvent eventModel, CancellationToken cancelToken)
    {
        var rooms = await _repository.GetRooms(true, cancelToken);

        if (rooms.IsError)
        {
            throw new NotImplementedException();
        }
        
        await _hubContext.Clients.All.SendAsync(nameof(RoomHub.SendOpenRooms), rooms.Value, CancellationToken.None);
    }
}