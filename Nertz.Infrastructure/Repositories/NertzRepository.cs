using System.Data;
using Dapper;
using ErrorOr;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.Contracts;
using Nertz.Infrastructure.DataModels;
using Npgsql;

namespace Nertz.Infrastructure.Repositories;

public class NertzRepository : INertzRepository
{
    private readonly string _connectionString;
    private readonly TimeProvider _timeProvider;
    public NertzRepository(IConfiguration configuration, TimeProvider timeProvider)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
        _timeProvider = timeProvider;
    }
    
    public async Task<ErrorOr<int>> CreateGame(GameDataModel game, CancellationToken cancelToken = default)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancelToken);

        await using var transaction = connection.BeginTransaction();
        try
        {
            var now = _timeProvider.GetUtcNow();
            
            var gameParameters = new DynamicParameters();
            gameParameters.Add("room_id", game.RoomId, dbType: DbType.Int32);;
            gameParameters.Add("created_at", now, dbType: DbType.DateTimeOffset);
            gameParameters.Add("new_game_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var createGameCommand = new CommandDefinition(StoredProcs.CreateGame, gameParameters, transaction, commandType: CommandType.StoredProcedure, cancellationToken: cancelToken);
            await connection.ExecuteAsync(createGameCommand);
            
            var newGameId = gameParameters.Get<int>("new_game_id");
            
            var roundParameters = new DynamicParameters();
            roundParameters.Add("game_id", newGameId, dbType: DbType.Int32);
            roundParameters.Add("round_number", 1, dbType: DbType.Int32);
            roundParameters.Add("target_score", game.TargetScore, dbType: DbType.Int32);
            roundParameters.Add("created_at", now, dbType: DbType.DateTimeOffset);
            roundParameters.Add("new_round_id", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var createRoundCommand = new CommandDefinition(StoredProcs.CreateGameRound, roundParameters, transaction, commandType: CommandType.StoredProcedure, cancellationToken: cancelToken);
            await connection.ExecuteAsync(createRoundCommand);
            
            var newRoundId = roundParameters.Get<int>("new_round_id");
            
            var playerParameters = new DynamicParameters();
            playerParameters.Add("round_id", newRoundId, dbType: DbType.Int32);
            playerParameters.Add("player_ids", game.PlayerIds, dbType: DbType.Object);
            var addPlayersToRoundCommand = new CommandDefinition(StoredProcs.AddPlayersToRound, playerParameters, transaction, commandType: CommandType.StoredProcedure, cancellationToken: cancelToken);
            await connection.ExecuteAsync(addPlayersToRoundCommand);

            await transaction.CommitAsync(cancelToken);

            return newGameId;
        }
        catch (Exception e)
        {
            transaction.Rollback();
            return GameErrors.UnableToCreateGame(e);
        }
    }

    public ErrorOr<int> CreateRound(int gameId, int roundNumber, DateTimeOffset createdAt, CancellationToken cancelToken = default)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<int> CreateCommonPile(int gameId, int roundId, Card card, CardRank rank, DateTimeOffset createdAt, CancellationToken cancelToken = default)
    {
        throw new NotImplementedException();
    }

    public ErrorOr<int> AddCardToCommonPile(int gameId, int roundId, int commonPileId, Card card, DateTimeOffset lastKnownUpdatedAt, CancellationToken cancelToken = default)
    {
        throw new NotImplementedException();
    }
}