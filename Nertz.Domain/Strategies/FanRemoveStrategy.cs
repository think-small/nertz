using Nertz.Domain.Contracts;
using Nertz.Domain.Cards;

namespace Nertz.Domain.Strategies;

public sealed class FanRemoveStrategy : BaseRemoveStrategy
{
    public override bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction)
    {
        cardTransaction = null;
        
        if (count != 1) return false;
        if (index != 0 && index != cardStack.Length - 1) return false;
        if (this.IsOutOfBounds(cardStack, index)) return false;

        cardTransaction = this.RemoveCards(cardStack, index, count);
        return true;
    }
}