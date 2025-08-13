using FastEndpoints;
using ErrorOr;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class CreateRoomHandler : ICommandHandler<CreateRoomCommand, ErrorOr<CreateRoomResponse>>
{
    private readonly IRoomRepository _repository;
    private readonly TimeProvider _timeProvider;

    public CreateRoomHandler(IRoomRepository repository, TimeProvider timeProvider)
    {
        _repository = repository;
        _timeProvider = timeProvider;
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

        await new RoomCreatedEvent().PublishAsync(Mode.WaitForNone, CancellationToken.None);
        
        return new CreateRoomResponse { RoomId = createRoomResponse.Value };
    }
}