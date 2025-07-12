namespace Nertz.Application.Nertz;

public class GameSetupOptions
{
    public const string NertzSetup = "Nertz:Setup";
    public int MaxPlayerCount { get; init; }
    public int MinPlayerCount { get; init; }
    public int MaxTargetScore { get; init; }
    public int MinTargetScore { get; init; }
    public int MaxFanSize { get; init; }
    public int MaxDrawPileSize { get; init; }
    public int MaxWorkPileSize { get; init; }
    public int MaxCommonPileSize { get; init; }
    public int WorkPilesPerPlayer { get; init; }
}