using Nertz.Domain.Contracts;
using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Strategies;

public sealed class CommonStackStrategy : IStackStrategy
{
    public bool CanStack(Card bottom, Card top)
    {
        if (bottom.Suit != top.Suit) return false;
        return bottom.Rank + 1 == top.Rank;
    }
}