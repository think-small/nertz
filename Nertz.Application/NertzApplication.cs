using ErrorOr;
using Microsoft.Extensions.Options;
using Nertz.Application.Contracts;
using Nertz.Application.Factories;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Application.Players;
using Nertz.Domain.ValueObjects;

namespace Nertz.Application.Nertz;

public class NertzApplication : INertz
{
    private readonly GameSetupOptions _setupOptions;
    private readonly IFactory<CardStack, CardStackType, Card> _cardStackFactory;
    private readonly IShuffle _shuffler;
    
    public NertzApplication(
        IOptions<GameSetupOptions> setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory,
        IShuffle shuffler)
    {
        _setupOptions = setupOptions.Value;
        _cardStackFactory = cardStackFactory;
        _shuffler = shuffler;
    }
    
    public ErrorOr<Game> SetupGame()
    {
        var players = Enumerable.Range(0, 2)
            .Select(_ => new Player
            {
                Hand = new PlayerHand
                {
                    Fan = _cardStackFactory.Create(CardStackType.NullCardStack),
                    DrawPile = _cardStackFactory.Create(CardStackType.NullCardStack),
                    WorkPiles = Enumerable.Range(0, 3).Select(_ => _cardStackFactory.Create(CardStackType.NullCardStack)).ToArray()
                }
            })
            .ToArray();
        
        return Game.CreateGame(1, 2, players, _setupOptions, _cardStackFactory, _shuffler);
    }
}