DROP FUNCTION IF EXISTS nertz.get_rooms;
CREATE FUNCTION nertz.get_rooms(
    open_only BOOLEAN = TRUE
)
RETURNS TABLE(
    Id INT,
    HostId INT,
    Name TEXT,
    MaxPlayerCount INT,
    TargetScore INT)
LANGUAGE sql
AS $$
    SELECT
        nertz.rooms.id,
        nertz.rooms.host_id,
        nertz.rooms.name,
        nertz.rooms.max_player_count,
        nertz.rooms.target_score
    FROM nertz.rooms
    WHERE open_only IS TRUE AND deleted_at IS NULL 
    OR open_only IS FALSE;
$$