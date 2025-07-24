namespace Nertz.Domain.Players;

public class Player
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public required PlayerHand Hand { get; set; }

    public Player() { }
    public Player(PlayerHand playerHand)
    {
        Id = Guid.NewGuid();
        Hand = playerHand;
    }
}