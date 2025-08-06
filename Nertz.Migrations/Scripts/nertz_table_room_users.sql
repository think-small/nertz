CREATE TABLE IF NOT EXISTS nertz.room_users(
    room_id INT NOT NULL,
    player_id INT NOT NULL,
    UNIQUE(room_id, player_id)
)