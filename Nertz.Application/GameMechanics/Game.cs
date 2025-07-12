using ErrorOr;
using Nertz.Application.Factories;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Application.Players;
using Nertz.Application.Shared.Errors;
using Nertz.Domain.ValueObjects;

namespace Nertz.Application.Nertz;

public sealed class Game
{
    private readonly GameSetupOptions _setupOptions;
    private readonly IFactory<CardStack, CardStackType, Card> _cardStackFactory;
    private readonly int _targetScore;
    private readonly int _maxPlayerCount;
    private readonly Player[] _players;
    private readonly CardStack[] _commonStacks;
    
    private Game(
        int targetScore, 
        int maxPlayerCount,
        Player[] players,
        GameSetupOptions setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory)
    {
        var suitCount = Enum.GetValues(typeof(Suit)).Length;
        _commonStacks = new CardStack[maxPlayerCount * suitCount];
        _targetScore = targetScore;
        _maxPlayerCount = maxPlayerCount;
        _players = players;
        _setupOptions = setupOptions;
        _cardStackFactory = cardStackFactory;
    }
    
    public static ErrorOr<Game> CreateGame(
        int targetScore,
        int maxPlayerCount,
        Player[] players,
        GameSetupOptions setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory,
        IShuffle shuffler)
    {
        var setupErrors = new List<Error>();
        
        if (targetScore < setupOptions.MinTargetScore || targetScore > setupOptions.MaxTargetScore)
            setupErrors.Add(GameSetupErrors.TargetScoreInvalid);
        if (maxPlayerCount < setupOptions.MinPlayerCount || maxPlayerCount > setupOptions.MaxPlayerCount)
            setupErrors.Add(GameSetupErrors.MaximumPlayerCountExceeded);
        
        if (players.Length < maxPlayerCount)
            setupErrors.Add(GameSetupErrors.InsufficientPlayerCount);
        if (players.Length > setupOptions.MinPlayerCount)
            setupErrors.Add(GameSetupErrors.MaximumPlayerCountExceeded);

        if (setupErrors.Any())
            return ErrorOr<Game>.From(setupErrors);

        foreach (var player in players)
        {
            player.Hand = CreateInitialPlayerHand(cardStackFactory, shuffler, setupOptions);
        }
        
        return new Game(targetScore, maxPlayerCount, players, setupOptions, cardStackFactory);
    }

    private static PlayerHand CreateInitialPlayerHand(
        IFactory<CardStack,CardStackType, Card> cardStackFactory,
        IShuffle shuffler,
        GameSetupOptions setupOptions)
    {
        var deck = Deck.CreateFullDeck(shuffler);
        var fanCards = deck.Take(setupOptions.MaxFanSize).ToArray();
        var workPileCrds = deck.Skip(setupOptions.MaxFanSize).Take(setupOptions.WorkPilesPerPlayer).ToArray();
        var drawPileCards = deck.Skip(fanCards.Length + workPileCrds.Length).Take(setupOptions.MaxDrawPileSize).ToArray();
        
        return new PlayerHand
        {
            Fan = cardStackFactory.Create(CardStackType.Fan, fanCards),
            DrawPile = cardStackFactory.Create(CardStackType.DrawPile, drawPileCards),
            WorkPiles = Enumerable
                .Range(0, setupOptions.WorkPilesPerPlayer)
                .Select((_, idx) => cardStackFactory.Create(CardStackType.WorkPile, [workPileCrds[idx]])).ToArray()
        };
    }
}