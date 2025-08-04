DROP PROCEDURE IF EXISTS nertz.create_room;
CREATE PROCEDURE nertz.create_room(
    name TEXT,
    created_at TIMESTAMP WITH TIME ZONE,
    OUT new_room_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.rooms(id, created_at, updated_at, deleted_at)
    VALUES (DEFAULT, created_at, created_at, NULL)
    RETURNING nertz.rooms.id INTO new_room_id;
END;
$$