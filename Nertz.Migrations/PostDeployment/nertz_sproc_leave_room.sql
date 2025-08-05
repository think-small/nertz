DROP PROCEDURE IF EXISTS nertz.leave_room;
CREATE PROCEDURE nertz.leave_room(
    room_id INT,
    player_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM nertz.room_players
    WHERE nertz.room_players.room_id = room_id AND nertz.room_players.player_id = player_id; 
END;
$$