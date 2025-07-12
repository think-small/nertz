using Microsoft.Extensions.Options;
using Nertz.Application.Nertz;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Domain.ValueObjects;
using Nertz.Domain.Strategies;

namespace Nertz.Application.Factories;

public enum CardStackType 
{
    NullCardStack = 0,
    Fan = 1,
    WorkPile = 2,
    DrawPile = 3,
    Common = 4
}

public class CardStackFactory : IFactory<CardStack, CardStackType, Card>
{
    private readonly GameSetupOptions _setupOptions;
    
    public CardStackFactory(IOptions<GameSetupOptions> setupOptions)
    {
        _setupOptions = setupOptions.Value;
    }
    
    public CardStack Create(CardStackType cardStackType, Card[]? cards = null)
    {
        cards ??= [];
        
        switch (cardStackType)
        {
            case CardStackType.Fan:
                return new CardStack(
                    new NoStackStrategy(),
                    new FanRemoveStrategy(),
                    _setupOptions.MaxFanSize,
                    cards);
            case CardStackType.WorkPile:
                return new CardStack(
                    new WorkPileStackStrategy(),
                    new WorkPileRemoveStrategy(),
                    _setupOptions.MaxWorkPileSize,
                    cards);
            case CardStackType.DrawPile:
                return new CardStack(
                    new NoStackStrategy(),
                    new DrawPileRemoveStrategy(),
                    _setupOptions.MaxDrawPileSize,
                    cards);
            case CardStackType.Common:
                return new CardStack(
                    new CommonStackStrategy(),
                    new NoRemoveStrategy(),
                    _setupOptions.MaxCommonPileSize,
                    cards);
            case CardStackType.NullCardStack:
                return new CardStack(
                    new NoStackStrategy(),
                    new NoRemoveStrategy(),
                    0);
            default:
                throw new ArgumentException("Unsupported card stack type");
        }
    }
}