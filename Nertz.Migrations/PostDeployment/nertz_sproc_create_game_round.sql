DROP PROCEDURE IF EXISTS nertz.create_game_round;
CREATE PROCEDURE nertz.create_game_round(
    game_id INTEGER,
    round_number INTEGER,
    target_score INTEGER,
    created_at TIMESTAMP WITH TIME ZONE,
    OUT new_round_id INTEGER
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.rounds(game_id, round_number, target_score, created_at, ended_at)
    VALUES (game_id, round_number, target_score, created_at, NULL)
    RETURNING nertz.rounds.id INTO new_round_id;
END;
$$