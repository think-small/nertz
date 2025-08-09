using FastEndpoints;
using ErrorOr;

namespace Nertz.Application.Nertz.Features.Rooms.GetOpenRooms;

public class GetOpenRoomsCommand : ICommand<ErrorOr<GetOpenRoomsResponse>>
{
    public bool IsOpen { get; init; } = true;
}