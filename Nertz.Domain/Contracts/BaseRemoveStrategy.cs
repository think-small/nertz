using Nertz.Domain.ValueObjects;

namespace Nertz.Domain.Contracts;

public abstract class BaseRemoveStrategy : IRemoveStrategy
{
    public abstract bool TryRemoveAt(Card[] cardStack, int index, int count, out CardTransaction? cardTransaction);

    protected CardTransaction RemoveCards(Card[] cardStack, int index, int count, bool shouldBypassOverdrawRules = false)
    {
        var modifiedCount = count;
        
        ArgumentOutOfRangeException.ThrowIfNegative(index);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(index, cardStack.Length - 1);
        if (shouldBypassOverdrawRules)
        {
            modifiedCount = Math.Min(count, cardStack.Length - index);
        }
        else
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThan(index + count, cardStack.Length);
        }

        return new CardTransaction
        {
            RemovedCards = cardStack.Skip(index).Take(modifiedCount).ToArray(),
            UpdatedCardState = [..cardStack.Take(index), ..cardStack.Skip(index + modifiedCount)]
        };
    }
    
    protected bool IsOutOfBounds(Card[] cardStack, int index)
    {
        return index < 0 || index > cardStack.Length - 1;
    }
}