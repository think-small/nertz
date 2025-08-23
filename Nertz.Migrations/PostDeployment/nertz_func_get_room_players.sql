DROP FUNCTION IF EXISTS nertz.get_room_players;
CREATE FUNCTION nertz.get_room_players(
    room_id INT
)
RETURNS TABLE(
    id INT,
    username TEXT,
    email TEXT,
    created_at TIMESTAMP WITH TIME ZONE,
    updated_at TIMESTAMP WITH TIME ZONE)
LANGUAGE sql
AS $$
    SELECT
        u.id,
        u.username,
        u.email,
        u.created_at,
        u.updated_at
    FROM nertz.users u
    JOIN nertz.room_users ru ON ru.player_id = u.id
    WHERE ru.room_id = room_id;
$$