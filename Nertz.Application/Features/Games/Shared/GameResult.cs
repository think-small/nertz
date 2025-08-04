namespace Nertz.Application.Nertz;

public sealed class GameResult
{
    public Dictionary<int, int> PlayerScores { get; init; } = new();
    public Guid WinnerId { get; set; }
}