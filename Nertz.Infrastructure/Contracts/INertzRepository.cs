using ErrorOr;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.DataModels;

namespace Nertz.Infrastructure.Contracts;

public interface INertzRepository
{
    Task<ErrorOr<int>> CreateGame(GameDataModel game);
    ErrorOr<int> CreateRound(int gameId, int roundNumber, DateTimeOffset createdAt);
    ErrorOr<int> CreateCommonPile(int gameId, int roundId, Card card, CardRank rank, DateTimeOffset createdAt);
    ErrorOr<int> AddCardToCommonPile(int gameId, int roundId, int commonPileId, Card card, DateTimeOffset lastKnownUpdatedAt);
}