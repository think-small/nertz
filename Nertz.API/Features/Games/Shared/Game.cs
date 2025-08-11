using System.Collections.Frozen;
using ErrorOr;
using Nertz.API.Features.Players;
using Nertz.API.Shared.Factories;
using Nertz.API.Shared.Interfaces;
using Nertz.API.Shared.Errors;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.DataModels;

namespace Nertz.API.Features.Games.Shared;

public sealed class Game
{
    private readonly int? _id;
    private readonly GameSetupOptions _setupOptions;
    private readonly IFactory<CardStack, CardStackType, Card> _cardStackFactory;
    private readonly int _targetScore;
    private readonly int _maxPlayerCount;
    private readonly Player[] _players;
    private readonly IList<GameRound> _rounds;
    private int _currentRoundNumber;
    private readonly GameResult _gameResult;
    public FrozenDictionary<int, int> Scores => _gameResult.PlayerScores.ToFrozenDictionary();
    
    private Game(
        GameSetupOptions setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory,
        int targetScore, 
        int maxPlayerCount,
        Player[] players,
        IList<GameRound> rounds,
        GameResult gameResult,
        int? id = null)
    {
        var suitCount = Enum.GetValues(typeof(Suit)).Length;
        _targetScore = targetScore;
        _maxPlayerCount = maxPlayerCount;
        _players = players;
        _setupOptions = setupOptions;
        _cardStackFactory = cardStackFactory;
        _rounds = rounds;
        _gameResult = gameResult;
        _id = id;
    }
    
    public static ErrorOr<Game> CreateGame(
        GameSetupOptions setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory,
        IShuffle shuffler,
        int targetScore,
        int maxPlayerCount,
        IEnumerable<int> playerIds,
        IList<GameRound>? rounds = null)
    {
        var setupErrors = new List<Error>();
        if (!playerIds.TryGetNonEnumeratedCount(out var playerCount))
        {
            playerCount = playerIds.Count();
        }
        
        if (playerCount > maxPlayerCount)
            setupErrors.Add(GameSetupErrors.MaximumPlayerCountExceeded);
        if (playerCount < setupOptions.MinPlayerCount)
            setupErrors.Add(GameSetupErrors.InsufficientPlayerCount);

        if (targetScore < setupOptions.MinTargetScore || targetScore > setupOptions.MaxTargetScore)
            setupErrors.Add(GameSetupErrors.TargetScoreInvalid);
        if (maxPlayerCount < setupOptions.MinPlayerCount || maxPlayerCount > setupOptions.MaxPlayerCount)
            setupErrors.Add(GameSetupErrors.MaximumPlayerCountExceeded);
        if (setupErrors.Any())
            return ErrorOr<Game>.From(setupErrors);

        var players = playerIds.Select(playerId => new Player
        {
            Id = playerId,
            Hand = CreateInitialPlayerHand(cardStackFactory, shuffler, setupOptions)
        }).ToArray();

        var gameResult = new GameResult
        {
            PlayerScores = CreateInitialPlayerScores(players)
        };
        
        return new Game(setupOptions, cardStackFactory, targetScore, maxPlayerCount, players, rounds ?? [], gameResult);
    }

    public GameDataModel ToDataModel()
    {
        return new GameDataModel
        {
            Id = _id,
            TargetScore = _targetScore,
            MaxPlayerCount = _maxPlayerCount,
            PlayerIds = _players.Select(p => p.Id).ToArray(),
            GameRounds = _rounds.Select(r => r.ToDataModel()).ToArray(),
        };
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

    private static Dictionary<int, int> CreateInitialPlayerScores(Player[] players)
    {
        return players.Select(p => new KeyValuePair<int, int>(p.Id, 0)).ToDictionary();
    }

    public ErrorOr<GameRound> StartNewRound()
    {
        var newRoundNumber = _currentRoundNumber + 1;
        var initialPlayerScores = CreateInitialPlayerScores(_players);
        var newRoundResult = GameRound.Create(_cardStackFactory, newRoundNumber, _targetScore, initialPlayerScores);

        if (newRoundResult.IsError)
        {
            return newRoundResult.Errors;
        }
        
        var newRound = newRoundResult.Value;
        
        _rounds.Add(newRound);
        _currentRoundNumber = newRoundNumber;

        return newRound;
    }
}