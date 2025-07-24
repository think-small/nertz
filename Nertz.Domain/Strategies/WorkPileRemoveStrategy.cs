using Nertz.Domain.Contracts;
using Nertz.Domain.Cards;

namespace Nertz.Domain.Strategies;

public sealed class WorkPileRemoveStrategy : BaseRemoveStrategy
{
    public override bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction)
    {
        cardTransaction = null;

        if (this.IsOutOfBounds(cardStack, index)) return false;
        if (index + count != cardStack.Length) return false;
        
        cardTransaction = this.RemoveCards(cardStack, index, count);
        return true;
    }
}