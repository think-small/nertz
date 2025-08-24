DROP FUNCTION IF EXISTS nertz.get_room_players;
CREATE FUNCTION nertz.get_room_players(
    room_id INT
)
RETURNS TABLE(
    Id INT,
    UserName TEXT,
    Email TEXT,
    IsHost BOOLEAN,
    CreatedAt TIMESTAMP WITH TIME ZONE,
    UpdatedAt TIMESTAMP WITH TIME ZONE)
LANGUAGE sql
AS $$
    SELECT
        u.id,
        u.username,
        u.email,
        r.host_id = u.id,
        u.created_at,
        u.updated_at
    FROM nertz.users u
    JOIN nertz.room_users ru ON ru.player_id = u.id
    JOIN nertz.rooms r ON r.id = room_id
    WHERE ru.room_id = room_id;
$$