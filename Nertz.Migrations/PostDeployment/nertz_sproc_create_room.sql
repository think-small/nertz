DROP PROCEDURE IF EXISTS nertz.create_room;
CREATE PROCEDURE nertz.create_room(
    name TEXT,
    host_id INT,
    max_player_count INT,
    created_at TIMESTAMP WITH TIME ZONE,
    OUT new_room_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.rooms(id, name, host_id, max_player_count, created_at, updated_at, deleted_at)
    VALUES (DEFAULT, name, host_id, max_player_count,created_at, created_at, NULL)
    RETURNING nertz.rooms.id INTO new_room_id;
END;
$$