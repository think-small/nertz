DROP PROCEDURE IF EXISTS nertz.create_game;
CREATE PROCEDURE nertz.create_game(
    room_id INT,
    created_at TIMESTAMP WITH TIME ZONE,
    OUT new_game_id INT 
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.games(id, room_id, created_at, updated_at, ended_at)
    VALUES (DEFAULT, room_id, created_at, created_at, NULL)
    RETURNING nertz.games.id INTO new_game_id;
END;
$$
