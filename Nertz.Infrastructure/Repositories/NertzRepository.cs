using Dapper;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.Contracts;
using Nertz.Infrastructure.DataModels;

namespace Nertz.Infrastructure.Repositories;

public class NertzRepository : INertzRepository
{
    private readonly string _connectionString;
    private readonly TimeProvider _timeProvider;
    public NertzRepository(IConfiguration configuration, TimeProvider timeprovider)
    {
        _connectionString = configuration.GetConnectionString("Nertz");
        _timeProvider = timeprovider;
    }
    
    public async Task<ErrorOr<int>> CreateGame(GameDataModel game)
    {
        await using var connection = new SqlConnection(_connectionString);
        connection.Open();

        await using var transaction = connection.BeginTransaction();
        try
        {
            var gameId = await transaction.Connection.ExecuteAsync(StoredProcs.CreateGame, new { created_at = _timeProvider.GetUtcNow() });
            await transaction.Connection.ExecuteAsync(StoredProcs.CreateGameRound, new { game_id = gameId, round_number = 0,});
            
            transaction.Commit();

            return gameId;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return GameErrors.UnableToCreateGame(e);
        }
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