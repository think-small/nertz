using Nertz.Domain.Contracts;
using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Strategies;

public sealed class NoStackStrategy : IStackStrategy
{
    public bool CanStack(Card bottom, Card top)
    {
        return false;
    }
}