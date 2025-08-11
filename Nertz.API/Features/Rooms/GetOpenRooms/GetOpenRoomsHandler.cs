using FastEndpoints;
using ErrorOr;
using Nertz.API.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Rooms;

public class GetOpenRoomsHandler : ICommandHandler<GetOpenRoomsCommand, ErrorOr<GetOpenRoomsResponse>>
{
    private readonly IRoomRepository _repository;

    public GetOpenRoomsHandler(IRoomRepository repository)
    {
        _repository = repository;
    }

    public async Task<ErrorOr<GetOpenRoomsResponse>> ExecuteAsync(GetOpenRoomsCommand command, CancellationToken cancelToken = default)
    {
        var roomsResponse= await _repository.GetRooms(shouldGetOnlyOpenRooms: command.IsOpen, cancelToken);

        if (roomsResponse.IsError)
        {
            throw new NotImplementedException();
        }

        return new GetOpenRoomsResponse { Rooms = roomsResponse.Value.Select(Room.FromDataModel) };
    }
}