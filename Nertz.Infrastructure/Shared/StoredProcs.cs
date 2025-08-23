namespace Nertz.Infrastructure;

public static class StoredProcs
{
    public const string CreateGame = "nertz.create_game";
    public const string CreateGameRound = "nertz.create_game_round";
    public const string AddPlayersToRound = "nertz.add_players_to_round";

    public const string CreateRoom = "nertz.create_room";
    public const string JoinRoom = "nertz.join_room";
    public const string LeaveRoom = "nertz.leave_room";
    public const string DeleteRoom = "nertz.delete_room";

    public const string GetUser = "nertz.get_user";
}