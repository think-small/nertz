CREATE TABLE IF NOT EXISTS nertz.rounds(
    id SERIAL PRIMARY KEY,
    game_id INT NOT NULL,
    round_number INT NOT NULL,
    target_score INT NOT NULL,
    created_at TIMESTAMP WITH TIME ZONE NOT NULL DEFAULT NOW(),
    ended_at TIMESTAMP WITH TIME ZONE DEFAULT NOW()
);