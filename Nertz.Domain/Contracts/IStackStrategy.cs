using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Contracts;

public interface IStackStrategy
{
    bool CanStack(Card bottom, Card top);
}