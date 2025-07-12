using Nertz.Domain.Contracts;
using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Strategies;

public sealed class WorkPileStackStrategy : IStackStrategy
{
    public bool CanStack(Card bottom, Card top)
    {
        if (isBlackSuit(bottom) == isBlackSuit(top)) return false;
        return bottom.Rank - 1 == top.Rank;
    }

    private static bool isBlackSuit(Card card)
    {
        return card.Suit is Suit.Spades or Suit.Clubs;
    }
}