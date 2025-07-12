using Nertz.Domain.Strategies;
using Nertz.Domain.ValueObjects;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class FanRemoveStrategyTests
{
    [Fact]
    public void Should_Not_Remove_If_Index_Is_Out_Of_Bounds()
    {
        var sut = new FanRemoveStrategy();

        var wasSuccess = sut.TryRemoveAt([], -1, 1, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Remove_If_Removing_More_Than_1_Card()
    {
        var sut = new FanRemoveStrategy();
        
        var wasSuccess = sut.TryRemoveAt([], 0, 2, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Remove_If_Not_First_Or_Last_Card()
    {
        var sut = new FanRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Hearts, Rank = CardRank.Ten },
            new Card { Suit = Suit.Diamonds, Rank = CardRank.Seven },
            new Card { Suit = Suit.Spades, Rank = CardRank.Jack },
        };
        
        var wasSuccess = sut.TryRemoveAt(cardStack, 1, 1, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Remove_First_Card()
    {
        var sut = new FanRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Hearts, Rank = CardRank.Ten },
            new Card { Suit = Suit.Diamonds, Rank = CardRank.Seven },
            new Card { Suit = Suit.Spades, Rank = CardRank.Jack },
        };
        
        var wasSuccess = sut.TryRemoveAt(cardStack, 0, 1, out var transaction);
        
        wasSuccess.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.RemovedCards.ShouldBe([cardStack.First()]);
        transaction.UpdatedCardState.ShouldBe(cardStack.Skip(1).Take(2));       
    }

    [Fact]
    public void Should_Remove_Last_Card()
    {
        var sut = new FanRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Hearts, Rank = CardRank.Ten },
            new Card { Suit = Suit.Diamonds, Rank = CardRank.Seven },
            new Card { Suit = Suit.Spades, Rank = CardRank.Jack },
        };

        var wasSuccess = sut.TryRemoveAt(cardStack, 2, 1, out var transaction);
        
        wasSuccess.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.RemovedCards.ShouldBe([cardStack.Last()]);
        transaction.UpdatedCardState.ShouldBe(cardStack.Take(2));
    }
}