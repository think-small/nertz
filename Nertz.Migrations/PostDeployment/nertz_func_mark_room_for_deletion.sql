DROP FUNCTION IF EXISTS nertz.mark_room_for_deletion;
CREATE FUNCTION nertz.mark_room_for_deletion(
    room_id INT,
    marked_for_deletion_at TIMESTAMP WITH TIME ZONE
) RETURNS VOID
LANGUAGE sql
AS $$
    UPDATE nertz.rooms
    SET deleted_at = marked_for_deletion_at
    WHERE id = room_id;
$$
