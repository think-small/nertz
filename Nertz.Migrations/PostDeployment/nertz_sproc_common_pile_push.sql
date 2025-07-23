CREATE OR REPLACE PROCEDURE nertz.common_pile_push(
    game_id INTEGER,
    round_id INTEGER,
    suit card_suits,
    new_rank card_ranks,
    created_at TIMESTAMP,
    last_known_updated_at TIMESTAMP,
    OUT success BOOLEAN,
    common_pile_id INTEGER DEFAULT NULL
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO nertz.common_piles(id, game_id, round_id, suit, current_rank, created_at, updated_at)
    VALUES (COALESCE(common_pile_id, nextval(pg_get_serial_sequence('nertz.common_piles', 'id'))), game_id, round_id, suit, new_rank, created_at, last_known_updated_at)
    ON CONFLICT (id) DO UPDATE
    SET current_rank = new_rank,
        updated_at = NOW()::timestamp
    WHERE nertz.common_piles.updated_at = last_known_updated_at;
    
    IF NOT FOUND THEN
        success := FALSE;
        RETURN;
    END IF;
    
    success := TRUE;
    RETURN;
END;
$$
