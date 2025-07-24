using ErrorOr;
using Nertz.Domain.Cards;
using Nertz.Domain.Players;
using Nertz.Infrastructure.Contracts;

namespace Nertz.Infrastructure.Repositories;

public class NertzRepository : INertzRepository
{
    public ErrorOr<int> CreateGame(Player[] players, DateTimeOffset createdAt)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<int> CreateRound(int gameId, int roundNumber, DateTimeOffset createdAt)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<int> CreateCommonPile(int gameId, int roundId, Card card, CardRank rank, DateTimeOffset createdAt)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<int> AddCardToCommonPile(int gameId, int roundId, int commonPileId, Card card, DateTimeOffset lastKnownUpdatedAt)
    {
        throw new NotImplementedException();
    }
}