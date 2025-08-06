using FastEndpoints;
using ErrorOr;

namespace Nertz.Application.Nertz.Features.Rooms.CreateRoom;

public class CreateRoomCommand : ICommand<ErrorOr<CreateRoomResponse>>
{
    public required string Name { get; init; }
    public required int HostId { get; init; }
    public int MaxPlayerCount { get; init; }
}