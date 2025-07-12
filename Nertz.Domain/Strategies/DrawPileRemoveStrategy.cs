using Nertz.Domain.Contracts;
using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Strategies;

public sealed class DrawPileRemoveStrategy : BaseRemoveStrategy
{
    public override bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction)
    {
        cardTransaction = null;
        
        if (count != 3) return false;
        if (index != 0) return false;
        
        cardTransaction = this.RemoveCards(cardStack, index, count, true);
        return true;
    }
}