using Nertz.Domain.Contracts;

namespace Nertz.Domain.ValueObjects;

 public sealed record CardStack
{
    private readonly IStackStrategy _stackStrategy;
    private readonly IRemoveStrategy _removeStrategy;
    private Card[] _cards;
    private int _lastFilledIndex = -1;
    public int Size => _lastFilledIndex + 1;

    public CardStack(IStackStrategy stackStrategy, IRemoveStrategy removeStrategy, int maxSize, Card[]? initialCards = null)
    {
        _stackStrategy = stackStrategy;
        _removeStrategy = removeStrategy;
        _cards = new Card[maxSize];
        
        if (initialCards is not null)
        {
            var idx = -1;
            foreach (var card in initialCards)
            {
                idx++;
                _cards[idx] = card;
            }
            
            _lastFilledIndex = idx;
        }
    }
    
    public bool AddToStack(Card card)
    {
        if (_lastFilledIndex is -1)
        {
            _lastFilledIndex++;
            _cards[_lastFilledIndex] = card;
            return true;
        }
        
        var canStack = _stackStrategy.CanStack(_cards[_lastFilledIndex], card);

        if (!canStack) return false;
        
        _lastFilledIndex++;
        _cards[_lastFilledIndex] = card;
        return true;
    }

    public bool TryRemoveAt(short index, short count, out CardStack? removedCardStack)
    {
        removedCardStack = null;
        
        if (!_removeStrategy.TryRemoveAt(_cards, index, count, out var cardTransaction) || cardTransaction is null) return false;
        
        _cards = cardTransaction.UpdatedCardState;
        _lastFilledIndex = _cards.Length - 1;
        removedCardStack = this with
        {
            _cards = cardTransaction.RemovedCards,
            _lastFilledIndex = cardTransaction.RemovedCards.Length - 1
        };
        
        return true;
    }
}