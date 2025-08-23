DROP FUNCTION IF EXISTS nertz.get_rooms;
CREATE FUNCTION nertz.get_rooms(
    open_only BOOLEAN = TRUE
)
RETURNS TABLE(
    Id INT,
    HostUserName TEXT,
    Name TEXT,
    MaxPlayerCount INT,
    TargetScore INT,
    CurrentPlayerCount INT)
LANGUAGE sql
AS $$
    SELECT
        r.id,
        u.username,
        r.name,
        r.max_player_count,
        r.target_score,
        (SELECT MAX(player_count) FROM (SELECT ROW_NUMBER() OVER (PARTITION BY room_id) AS player_count FROM nertz.room_users) as rnk) as current_player_count
    FROM nertz.rooms r
    JOIN nertz.users u ON u.id = r.host_id
    WHERE open_only IS TRUE AND deleted_at IS NULL 
    OR open_only IS FALSE;
$$