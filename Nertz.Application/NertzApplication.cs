using ErrorOr;
using Microsoft.Extensions.Options;
using Nertz.Application.Contracts;
using Nertz.Application.Factories;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Domain.Cards;

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
    
    public ErrorOr<Game> SetupGame(int targetScore, int maxPlayerCount, IEnumerable<Guid> playerIds)
    {
        return Game.CreateGame(_setupOptions, _cardStackFactory, _shuffler, targetScore, maxPlayerCount, playerIds.ToArray());
    }
}