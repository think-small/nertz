DROP PROCEDURE IF EXISTS nertz.create_game;
CREATE PROCEDURE nertz.create_game(
    created_at TIMESTAMP WITH TIME ZONE,
    name TEXT,
    OUT new_game_id INT 
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.games(id, name, created_at, updated_at, ended_at)
    VALUES (DEFAULT, name, created_at, created_at, NULL)
    RETURNING nertz.games.id INTO new_game_id;
END;
$$
