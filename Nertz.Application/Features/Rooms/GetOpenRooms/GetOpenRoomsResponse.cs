using Nertz.Application.Nertz.Features.Rooms.Shared;

namespace Nertz.Application.Nertz.Features.Rooms.GetOpenRooms;

public class GetOpenRoomsResponse
{
    public IEnumerable<Room> Rooms { get; init; } = [];
}