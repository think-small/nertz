using FastEndpoints;
using ErrorOr;

namespace Nertz.API.Features.Rooms;

public class CreateRoomCommand : ICommand<ErrorOr<CreateRoomResponse>>
{
    public required string Name { get; init; }
    public required int HostId { get; init; }
    public int MaxPlayerCount { get; init; }
    public int TargetScore { get; init; }
}