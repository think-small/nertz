DROP PROCEDURE IF EXISTS nertz.join_room;
CREATE PROCEDURE nertz.join_room(
    room_id INT,
    player_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.room_users(room_id, player_id)
    VALUES (room_id, player_id);
END;
$$