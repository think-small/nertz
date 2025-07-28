using Nertz.Application.Shared.Interfaces;
using Nertz.Domain.Cards;

namespace Nertz.Application.Nertz;

public static class Deck
{
    public const int DECK_SIZE = 52;
    public const int CARDS_PER_SUIT = 13;
    public const int SUITS_PER_DECK = 4;

    public static Card[] CreateFullDeck(IShuffle shuffler)
    {
        var deck = new Card[DECK_SIZE];
        var idx = 0;
        
        foreach (var suit in Enum.GetValues<Suit>())
        {
            foreach (var rank in Enum.GetValues<CardRank>())
            {
                deck[idx] = new Card { Suit = suit, Rank = rank };
                idx++;
            }
        }

        shuffler.Shuffle(deck);
        return deck;
    }
}