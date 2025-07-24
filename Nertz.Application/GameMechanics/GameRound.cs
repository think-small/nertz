using ErrorOr;
using Nertz.Application.Factories;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Application.Shared.Errors;
using Nertz.Domain.Cards;

namespace Nertz.Application.Nertz;

public class GameRound
{
    private readonly int _targetScore;
    public  Guid Id { get; init; }
    public Guid GameId { get; init; }
    public int RoundNumber { get; init; }
    public CardStack[] CommonPiles { get; init; }
    public Dictionary<Guid, int> PlayerScores { get; init; }
    public Guid? RoundWinnerId
    {
        get
        {
            var leadingPlayer = PlayerScores.MaxBy(playerMap => playerMap.Value);
            return leadingPlayer.Value >= _targetScore ? leadingPlayer.Key : null;
        }
    }

    private GameRound(Guid id, Guid gameId, int roundNumber, int targetScore, Dictionary<Guid, int> playerScores, CardStack[] commonPiles)
    {
        Id = id;
        GameId = gameId;
        RoundNumber = roundNumber;
        PlayerScores = playerScores;
        CommonPiles = commonPiles;
        _targetScore = targetScore;
    }

    public static ErrorOr<GameRound> Create(IFactory<CardStack, CardStackType, Card> cardStackFactory, Guid gameId, int roundNumber, int targetScore, Dictionary<Guid, int> playerScores, Guid? id = null, CardStack[]? commonPiles = null)
    {
        try
        {
            commonPiles ??= InitializeCommonPiles(cardStackFactory, playerScores.Count);
        }
        catch
        {
            return RoundSetupErrors.UnableToInitializeCommonPile;
        }
        
        return new GameRound(
            id ?? Guid.NewGuid(),
            gameId,
            roundNumber,
            targetScore,
            playerScores,
            commonPiles);
    }

    private static CardStack[] InitializeCommonPiles(IFactory<CardStack, CardStackType, Card> cardStackFactory, int playerCount)
    {
        return Enumerable.Range(0, Deck.SUITS_PER_DECK * playerCount).Select(_ => cardStackFactory.Create(CardStackType.Common)).ToArray();
    }
}