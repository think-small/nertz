using ErrorOr;
using Nertz.Domain.Players;
using Nertz.Domain.Cards;

namespace Nertz.Infrastructure.Contracts;

public interface INertzRepository
{
    ErrorOr<int> CreateGame(Player[] players, DateTimeOffset createdAt);
    ErrorOr<int> CreateRound(int gameId, int roundNumber, DateTimeOffset createdAt);
    ErrorOr<int> CreateCommonPile(int gameId, int roundId, Card card, CardRank rank, DateTimeOffset createdAt);
    ErrorOr<int> AddCardToCommonPile(int gameId, int roundId, int commonPileId, Card card, DateTimeOffset lastKnownUpdatedAt);
}