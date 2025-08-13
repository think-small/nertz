namespace Nertz.Infrastructure.DataModels;

public class GameDataModel
{
    public int? Id { get; init; }
    public int RoomId { get; init; }
    public int TargetScore { get; init; }
    public int MaxPlayerCount { get; init; }
    public required int[] PlayerIds { get; init; }
    public required GameRoundDataModel[] GameRounds { get; init; }
    public DateTimeOffset? CreatedAt { get; init; }
    public DateTimeOffset? UpdatedAt { get; init; }
    public DateTimeOffset? EndedAt { get; init; }
}