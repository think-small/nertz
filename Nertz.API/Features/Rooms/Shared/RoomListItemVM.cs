using Nertz.Infrastructure.DataModels;

namespace Nertz.API.Features.Rooms.Shared;

public class RoomListItemVM
{
    public int Id { get; init; }
    public required string HostUserName { get; init; }
    public required string Name { get; init; }
    public int MaxPlayerCount { get; init; }
    public int TargetScore { get; init; }
    public int CurrentPlayerCount { get; init; }

    public static RoomListItemVM FromDataModel(RoomListItemDataModel roomListItemDataModel)
    {
        return new RoomListItemVM
        {
            Id = roomListItemDataModel.Id,
            HostUserName = roomListItemDataModel.HostUserName,
            Name = roomListItemDataModel.Name,
            MaxPlayerCount = roomListItemDataModel.MaxPlayerCount,
            TargetScore = roomListItemDataModel.TargetScore,
            CurrentPlayerCount = roomListItemDataModel.CurrentPlayerCount
        };
    }
}