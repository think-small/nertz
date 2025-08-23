namespace Nertz.Infrastructure.DataModels;

public class RoomListItemDataModel
{
    public int Id { get; init; }
    public required string HostUserName { get; init; }
    public required string Name { get; init; }
    public int MaxPlayerCount { get; init; }
    public int TargetScore { get; init; }
    public int CurrentPlayerCount { get; init; }
}