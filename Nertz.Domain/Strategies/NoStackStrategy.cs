using Nertz.Domain.Contracts;
using Nertz.Domain.Cards;

namespace Nertz.Domain.Strategies;

public sealed class NoStackStrategy : IStackStrategy
{
    public bool CanStack(Card bottom, Card top)
    {
        return false;
    }
}