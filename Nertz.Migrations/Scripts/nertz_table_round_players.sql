CREATE TABLE IF NOT EXISTS nertz.round_players(
    id SERIAL PRIMARY KEY,
    round_id INT NOT NULL,
    player_id INT NOT NULL
)