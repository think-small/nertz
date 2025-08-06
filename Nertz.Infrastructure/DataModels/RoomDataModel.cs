namespace Nertz.Infrastructure.DataModels;

public class RoomDataModel
{
    public int Id { get; init; }
    public int HostId { get; init; }
    public required string Name { get; init; }
    public int MaxPlayerCount { get; init; }
    public int TargetScore { get; init; }
}