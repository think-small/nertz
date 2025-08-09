using Nertz.Infrastructure.DataModels;

namespace Nertz.Application.Nertz.Features.Rooms.Shared;

public class Room
{
    public int Id { get; init; }
    public int HostId { get; init; }
    public string Name { get; init; }
    public int MaxPlayerCount { get; init; }
    public int TargetScore { get; init; }

    public static Room FromDataModel(RoomDataModel roomDataModel)
    {
        return new Room
        {
            Id = roomDataModel.Id,
            HostId = roomDataModel.HostId,
            Name = roomDataModel.Name,
            MaxPlayerCount = roomDataModel.MaxPlayerCount,
            TargetScore = roomDataModel.TargetScore
        };
    }
}