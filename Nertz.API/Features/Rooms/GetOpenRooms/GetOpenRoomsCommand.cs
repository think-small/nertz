using FastEndpoints;
using ErrorOr;

namespace Nertz.API.Features.Rooms;

public class GetOpenRoomsCommand : ICommand<ErrorOr<GetOpenRoomsResponse>>
{
    public bool IsOpen { get; init; } = true;
}