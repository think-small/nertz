using FastEndpoints;
using ErrorOr;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class LeaveRoomHandler : ICommandHandler<LeaveRoomCommand, ErrorOr<LeaveRoomResponse>>
{
    private readonly IRoomRepository _repository;
    private readonly TimeProvider _timeProvider;

    public LeaveRoomHandler(IRoomRepository repository, TimeProvider timeProvider)
    {
        _repository = repository;
        _timeProvider = timeProvider;
    }
    
    public async Task<ErrorOr<LeaveRoomResponse>> ExecuteAsync(LeaveRoomCommand command,
        CancellationToken cancelToken)
    {
        var roomPlayersResponse = await _repository.GetRoomPlayers(command.RoomId, cancelToken);

        if (roomPlayersResponse.IsError)
        {
            return roomPlayersResponse.Errors;
        }

        var shouldRoomBeDeleted = roomPlayersResponse.Value.Count() == 1;
        var isHost = roomPlayersResponse.Value.Single(p => p.IsHost).Id == command.PlayerId;

        if (shouldRoomBeDeleted)
        {
            var deleteRoomResponse = await _repository.MarkRoomForDeletion(command.RoomId, _timeProvider.GetUtcNow(), cancelToken);
            
            if (deleteRoomResponse.IsError)
            {
                return deleteRoomResponse.Errors;
            }

            await new RoomListUpdatedEvent().PublishAsync(Mode.WaitForNone, CancellationToken.None);
            
            return new LeaveRoomResponse();
        }

        if (isHost)
        {
            var newHost = roomPlayersResponse.Value.OrderBy(p => p.Id).First();
            await _repository.AssignHost(command.RoomId, newHost.Id, cancelToken);
        }
        
        var leaveResponse = await _repository.LeaveRoom(command.RoomId, command.PlayerId, cancelToken);

        if (leaveResponse.IsError)
        {
            return leaveResponse.Errors;
        }
        
        await new RoomPlayersChangedEvent{ RoomId = command.RoomId }.PublishAsync(Mode.WaitForNone, CancellationToken.None);
        
        return new LeaveRoomResponse();
    }
}