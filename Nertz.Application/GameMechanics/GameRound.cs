using ErrorOr;
using Nertz.Application.Shared.Errors;
using Nertz.Application.Shared.Factories;
using Nertz.Application.Shared.Interfaces;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.DataModels;

namespace Nertz.Application.Nertz;

public class GameRound
{
    private readonly int _targetScore;
    public int? Id { get; init; }
    public int? GameId { get; init; }
    public int RoundNumber { get; init; }
    public CardStack[] CommonPiles { get; init; }
    public Dictionary<int, int> PlayerScores { get; init; }

    public int? RoundWinnerId
    {
        get
        {
            var leadingPlayer = PlayerScores.MaxBy(playerMap => playerMap.Value);
            return leadingPlayer.Value >= _targetScore ? leadingPlayer.Key : null;
        }
    }

    private GameRound(int roundNumber, int targetScore, Dictionary<int, int> playerScores, CardStack[] commonPiles,
        int? id = null, int? gameId = null)
    {
        Id = id;
        GameId = gameId;
        RoundNumber = roundNumber;
        PlayerScores = playerScores;
        CommonPiles = commonPiles;
        _targetScore = targetScore;
    }

    public static ErrorOr<GameRound> Create(IFactory<CardStack, CardStackType, Card> cardStackFactory, int roundNumber,
        int targetScore, Dictionary<int, int> playerScores, int? id = null, int? gameId = null,
        CardStack[]? commonPiles = null)
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
            roundNumber,
            targetScore,
            playerScores,
            commonPiles,
            gameId,
            id);
    }

    private static CardStack[] InitializeCommonPiles(IFactory<CardStack, CardStackType, Card> cardStackFactory,
        int playerCount)
    {
        return Enumerable.Range(0, Deck.SUITS_PER_DECK * playerCount)
            .Select(_ => cardStackFactory.Create(CardStackType.Common)).ToArray();
    }

    public GameRoundDataModel ToDataModel()
    {
        return new GameRoundDataModel();
    }
}