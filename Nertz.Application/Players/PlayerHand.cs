using Nertz.Domain.Cards;

namespace Nertz.Application.Players;

public sealed class PlayerHand
{
    public required CardStack Fan { get; init; }
    public required CardStack[] WorkPiles { get; init; }
    public required CardStack DrawPile { get; init; }
}