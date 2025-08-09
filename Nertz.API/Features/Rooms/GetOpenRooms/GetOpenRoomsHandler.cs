using FastEndpoints;
using ErrorOr;
using Nertz.Application.Nertz.Features.Rooms.Shared;
using Nertz.Infrastructure.Contracts;

namespace Nertz.Application.Nertz.Features.Rooms.GetOpenRooms;

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