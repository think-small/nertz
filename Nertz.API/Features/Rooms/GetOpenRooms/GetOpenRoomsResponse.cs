using Nertz.API.Features.Rooms.Shared;

namespace Nertz.API.Features.Rooms;

public class GetOpenRoomsResponse
{
    public IEnumerable<Room> Rooms { get; init; } = [];
}