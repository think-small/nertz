DROP FUNCTION IF EXISTS nertz.assign_room_host;
CREATE FUNCTION nertz.assign_room_host(
    room_id INT,
    new_host_id INT
) RETURNS VOID
LANGUAGE sql
AS $$
    UPDATE nertz.rooms
    SET host_id = new_host_id 
    WHERE id = room_id;
$$
