CREATE TABLE IF NOT EXISTS nertz.round_players(
    round_id INT NOT NULL,
    player_id INT NOT NULL,
    UNIQUE (round_id, player_id)
)