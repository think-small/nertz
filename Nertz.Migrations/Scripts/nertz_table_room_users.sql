CREATE TABLE IF NOT EXISTS nertz.room_users(
    roomId INT NOT NULL,
    userId INT NOT NULL,
    UNIQUE(roomId, userId)
)