DROP PROCEDURE IF EXISTS nertz.add_players_to_round;
CREATE PROCEDURE nertz.add_players_to_round(
    round_id INTEGER,
    player_ids INTEGER[]
)
LANGUAGE  plpgsql
AS $$
BEGIN
    INSERT INTO nertz.round_players(round_id, player_id)
    SELECT round_id, unnest(player_ids);
END;
$$