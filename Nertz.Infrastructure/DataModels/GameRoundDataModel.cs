using Nertz.Domain.Cards;

namespace Nertz.Infrastructure.DataModels;

public class GameRoundDataModel
{
    public int Id { get; init; }
    public int? GameId { get; init; }
    public int RoundNumber { get; init; }
    public int TargetScore { get; init; }
    public Dictionary<int, int> PlayerScores { get; init; }
    public CardStack[] CommonPiles { get; init; }
}