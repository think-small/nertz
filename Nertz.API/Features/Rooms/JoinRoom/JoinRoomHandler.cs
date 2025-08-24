using FastEndpoints;
using ErrorOr;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.Application.Features.Rooms.JoinRoom;

public class JoinRoomHandler : ICommandHandler<JoinRoomCommand, ErrorOr<JoinRoomResponse>>
{
    private readonly IRoomRepository _repository;

    public JoinRoomHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<JoinRoomResponse>> ExecuteAsync(JoinRoomCommand command, CancellationToken cancelToken = default)
    {
        var joinRoomResponse = await _repository.JoinRoom(command.RoomId, command.PlayerId, cancelToken);

        if (joinRoomResponse.IsError)
        {
            throw new NotImplementedException();
        }

        var joinedRoomEvent = new RoomPlayersChangedEvent { RoomId = command.RoomId };
        await joinedRoomEvent.PublishAsync(Mode.WaitForNone, CancellationToken.None);
        
        return new JoinRoomResponse();
    }
}