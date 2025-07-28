DROP PROCEDURE IF EXISTS nertz.create_game;
CREATE PROCEDURE nertz.create_game(
    created_at TIMESTAMP,
    OUT new_game_id INT 
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.games(created_at, updated_at, ended_at)
    VALUES (created_at, created_at, NULL)
    RETURNING nertz.games.id INTO new_game_id;
END;
$$
