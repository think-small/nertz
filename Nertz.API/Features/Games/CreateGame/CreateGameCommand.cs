using ErrorOr;
using FastEndpoints;

namespace Nertz.Application.Nertz.Features.Games;

public class CreateGameCommand : ICommand<ErrorOr<CreateGameResponse>>
{
    public int TargetScore { get; init; }
    public int MaxPlayerCount { get; init; }
    public required int[] PlayerIds { get; init; }
}