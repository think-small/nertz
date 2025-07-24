using Nertz.Domain.Cards;

namespace Nertz.Domain.Contracts;

public interface IRemoveStrategy
{
    bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction);
}