using FastEndpoints;
using ErrorOr;
using Microsoft.AspNetCore.SignalR;
using Nertz.Infrastructure.Contracts;

namespace Nertz.Application.Nertz.Features.Rooms.CreateRoom;

public class CreateRoomHandler : ICommandHandler<CreateRoomCommand, ErrorOr<CreateRoomResponse>>
{
    private readonly IRoomRepository _repository;
    private readonly TimeProvider _timeProvider;
    private readonly IHubContext<RoomHub> _hubContext;

    public CreateRoomHandler(IRoomRepository repository, TimeProvider timeProvider, IHubContext<RoomHub> hubContext)
    {
        _repository = repository;
        _timeProvider = timeProvider;
        _hubContext = hubContext;
    }
    
    public async Task<ErrorOr<CreateRoomResponse>> ExecuteAsync(CreateRoomCommand command, CancellationToken cancelToken = default)
    {
        var createRoomResponse = await _repository.CreateRoom(command.Name, command.HostId, command.MaxPlayerCount, command.TargetScore, _timeProvider.GetUtcNow(), cancelToken);

        if (createRoomResponse.IsError)
        {
            throw new NotImplementedException();
        }

        var hostJoinRoomResponse = await _repository.JoinRoom(createRoomResponse.Value, command.HostId, cancelToken);

        if (hostJoinRoomResponse.IsError)
        {
            throw new NotImplementedException();
        }

        // TODO - send list of updated available rooms to clients.
        //        Since the list of rooms may be changing frequently,
        //        consider use of a pessimistic lock for retrieval.
        return new CreateRoomResponse { RoomId = createRoomResponse.Value };
    }
}