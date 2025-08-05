using System.Data;
using Dapper;
using ErrorOr;
using Microsoft.Extensions.Configuration;
using Nertz.Infrastructure.Contracts;
using Npgsql;

namespace Nertz.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly string _connectionString;
    
    public RoomRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection") ?? string.Empty;
    }
    
    public async Task<ErrorOr<int>> CreateRoom(string name, DateTimeOffset createdAt, CancellationToken cancelToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancelToken);
        
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("created_at", createdAt, dbType: DbType.DateTimeOffset);
            parameters.Add("name", name, dbType: DbType.String);
            parameters.Add("new_room_id", dbType: DbType.Int32, direction: ParameterDirection.Output);;
            var createRoomCommand = new CommandDefinition(
                StoredProcs.CreateRoom,
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancelToken);
            
            await connection.ExecuteAsync(createRoomCommand);
            
            return parameters.Get<int>("new_room_id");
        }
        catch (Exception e)
        {
            return RoomErrors.UnableToCreateRoom(e);
        }
    }

    public async Task<ErrorOr<bool>> JoinRoom(int roomId, int playerId, CancellationToken cancelToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancelToken);

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("room_id", roomId, dbType: DbType.Int32);
            parameters.Add("player_id", playerId, dbType: DbType.Int32);
            var joinRoomCommand = new CommandDefinition(
                StoredProcs.JoinRoom,
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancelToken);
            
            await connection.ExecuteAsync(joinRoomCommand);

            return true;
        }
        catch (Exception e)
        {
            return RoomErrors.UnableToJoinRoom(e);
        }
    }

    public async Task<ErrorOr<bool>> LeaveRoom(int roomId, int playerId, CancellationToken cancelToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancelToken);

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("room_id", roomId, dbType: DbType.Int32);
            parameters.Add("player_id", playerId, dbType: DbType.Int32);
            var leaveCommand = new CommandDefinition(
                StoredProcs.LeaveRoom,
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancelToken);
            
            await connection.ExecuteAsync(leaveCommand);

            return true;
        }
        catch (Exception e)
        {
            return RoomErrors.UnableToLeaveRoom(e);
        }
    }

    public async Task<ErrorOr<bool>> DeleteRoom(int roomId, CancellationToken cancelToken)
    {
        await using var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync(cancelToken);

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("room_id", roomId, dbType: DbType.Int32);
            var deleteRoomCommand = new CommandDefinition(
                StoredProcs.DeleteRoom,
                parameters,
                commandType: CommandType.StoredProcedure,
                cancellationToken: cancelToken);
            
            await connection.ExecuteAsync(deleteRoomCommand);

            return true;
        }
        catch (Exception e)
        {
            return RoomErrors.UnableToDeleteRoom(e);
        }
    }
}