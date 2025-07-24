using Nertz.Domain.Cards;

namespace Nertz.Domain.Players;

public sealed class PlayerHand
{
    public required CardStack Fan { get; init; }
    public required CardStack[] WorkPiles { get; init; }
    public required CardStack DrawPile { get; init; }
}