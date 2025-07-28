using Nertz.Application.Nertz;
using Nertz.Application.Shared.Interfaces;
using Nertz.Domain.Cards;
using NSubstitute;
using Shouldly;

namespace Nertz.Tests.Application.NertzTests;

public class DeckTests
{
    private readonly IShuffle _mockShuffle = Substitute.For<IShuffle>();

    [Fact]
    public void Should_Create_All_Suits_With_All_Card_Ranks()
    {
        var actual = Deck.CreateFullDeck(_mockShuffle);
        
        actual.Length.ShouldBe(Deck.DECK_SIZE);
        actual.Count(card => card.Suit == Suit.Clubs).ShouldBe(Deck.CARDS_PER_SUIT);
        actual.Count(card => card.Suit == Suit.Spades).ShouldBe(Deck.CARDS_PER_SUIT);
        actual.Count(card => card.Suit == Suit.Hearts).ShouldBe(Deck.CARDS_PER_SUIT);
        actual.Count(card => card.Suit == Suit.Diamonds).ShouldBe(Deck.CARDS_PER_SUIT);
    }
}