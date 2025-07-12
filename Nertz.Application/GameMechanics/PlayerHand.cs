using Nertz.Domain.ValueObjects;

namespace Nertz.Application.Nertz;

public sealed class PlayerHand
{
    public required CardStack Fan { get; init; }
    public required CardStack[] WorkPiles { get; init; }
    public required CardStack DrawPile { get; init; }
}