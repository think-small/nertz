namespace Nertz.Domain.Cards;

public enum Suit
{
    Spades = 1,
    Clubs = 2,
    Diamonds = 3,
    Hearts = 4
}

public enum CardRank
{
    Ace = 1,
    Two = 2,
    Three = 3,
    Four = 4,
    Five = 5,
    Six = 6,
    Seven = 7,
    Eight = 8,
    Nine = 9,
    Ten = 10,
    Jack = 11,
    Queen = 12,
    King = 13
}

public sealed record Card
{
    public Suit Suit { get; init; }
    public CardRank Rank { get; init; }
}