namespace Nertz.Application.Players;

public class Player
{
    public int Id { get; init; }
    public required PlayerHand Hand { get; set; }

    public Player() { }
    public Player(PlayerHand playerHand, int id)
    {
        Hand = playerHand;
        Id = id;
    }
}