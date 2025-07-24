using Nertz.Domain.Contracts;
using Nertz.Domain.Cards;

namespace Nertz.Domain.Strategies;

public sealed class NoRemoveStrategy : BaseRemoveStrategy
{
    public override bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction)
    {
        cardTransaction = null;
        return false;
    }
}