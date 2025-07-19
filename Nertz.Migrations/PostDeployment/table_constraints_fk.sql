DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'nertz' AND TABLE_NAME = 'rounds') THEN
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA = 'nertz' AND CONSTRAINT_NAME = 'fk_rounds_games' AND CONSTRAINT_TYPE = 'FOREIGN KEY') THEN
            ALTER TABLE nertz.rounds
            ADD CONSTRAINT fk_rounds_games FOREIGN KEY (game_id)
            REFERENCES nertz.games(id);
        END IF;
    END IF;
END $$;

DO $$
BEGIN
    IF EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'nertz' AND TABLE_NAME = 'common_piles') THEN
        IF NOT EXISTS (SELECT 1 FROM INFORMATION_SCHEMA.TABLE_CONSTRAINTS WHERE TABLE_SCHEMA = 'nertz' AND CONSTRAINT_NAME = 'fk_common_piles_rounds') THEN
            ALTER TABLE nertz.common_piles
            ADD CONSTRAINT fk_common_piles_rounds FOREIGN KEY (round_id)
            REFERENCES nertz.rounds(id);
        END IF;
    END IF;
END $$;