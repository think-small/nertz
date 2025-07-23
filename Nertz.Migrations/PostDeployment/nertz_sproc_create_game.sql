CREATE OR REPLACE PROCEDURE nertz.create_game(
    created_at TIMESTAMP
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.games(created_at, updated_at, ended_at)
    VALUES (created_at, created_at, NULL);
END;
$$
