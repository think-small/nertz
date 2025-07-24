using Nertz.Domain.Cards;

namespace Nertz.Domain.Contracts;

public interface IStackStrategy
{
    bool CanStack(Card bottom, Card top);
}