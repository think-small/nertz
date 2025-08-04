using Nertz.Application.Players;

namespace Nertz.Application.Nertz.Features.CreateGame;

public class CreateGameRequest
{
    public int TargetScore { get; init; }
    public int MaxPlayerCount { get; init; }
    public required int[] PlayerIds { get; init; }
}