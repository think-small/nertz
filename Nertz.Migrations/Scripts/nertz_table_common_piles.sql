CREATE TABLE IF NOT EXISTS nertz.common_piles(
    id serial PRIMARY KEY,
    round_id INT,
    suit card_suits,
    current_rank card_ranks,
    created_at TIMESTAMP NOT NULL DEFAULT NOW(),
    updated_at TIMESTAMP NULL DEFAULT NOW()
);