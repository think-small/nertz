DROP PROCEDURE IF EXISTS nertz.delete_room;
CREATE PROCEDURE nertz.delete_room(
    room_id INT
)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM nertz.room_users
    WHERE nertz.room_users.room_id = room_id;
    
    DELETE FROM nertz.rooms
    WHERE id = room_id;
END;
$$