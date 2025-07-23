CREATE OR REPLACE PROCEDURE nertz.create_game_round(
    game_id INTEGER,
    round_number INTEGER,
    created_at TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.rounds(game_id, round_number, created_at, ended_at)
    VALUES (game_id, round_number, created_at, NULL);
END;
$$