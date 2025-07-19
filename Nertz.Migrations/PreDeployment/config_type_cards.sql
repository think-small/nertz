DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'card_suits') THEN
            CREATE TYPE card_suits AS ENUM ('clubs', 'diamonds', 'hearts', 'spades');
        END IF;
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'card_ranks') THEN
            CREATE TYPE card_ranks AS ENUM ('ace', '2', '3', '4', '5', '6', '7', '8', '9', '10', 'jack', 'queen', 'king');
        END IF;
END $$