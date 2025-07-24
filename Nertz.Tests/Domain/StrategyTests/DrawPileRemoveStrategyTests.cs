using Nertz.Domain.Strategies;
using Nertz.Domain.Cards;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class DrawPileRemoveStrategyTests
{
    [Fact]
    public void Should_Not_Remove_If_Card_Is_Out_Of_Bounds()
    {
        var sut = new DrawPileRemoveStrategy();
        
        var wasRemoved = sut.TryRemoveAt([], 1, 3, out var transaction);
        
        wasRemoved.ShouldBeFalse();
        transaction.ShouldBeNull();
    }
    
    [Fact]
    public void Should_Not_Remove_If_Count_Is_Not_3()
    {
        var sut = new DrawPileRemoveStrategy();
        
        var wasRemoved = sut.TryRemoveAt([], 0, 0, out var transaction);
        
        wasRemoved.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Remove_If_Index_Is_Not_0()
    {
        var sut = new DrawPileRemoveStrategy();
        
        var wasRemoved = sut.TryRemoveAt([], 1, 3, out var transaction);
        
        wasRemoved.ShouldBeFalse();
        transaction.ShouldBeNull();
    }
    
    [Fact]
    public void Should_Remove_If_Count_Is_3_And_Index_Is_0()
    {
        var sut = new DrawPileRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Spades, Rank = CardRank.Ace },
            new Card { Suit = Suit.Spades, Rank = CardRank.Two },
            new Card { Suit = Suit.Spades, Rank = CardRank.Three },
        };
        
        var wasRemoved = sut.TryRemoveAt(cardStack, 0, 3, out var transaction);
        
        wasRemoved.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.RemovedCards.ShouldBe(cardStack);
        transaction.UpdatedCardState.ShouldBeEmpty();       
    }

    [Fact]
    public void Should_Remove_Whatever_Cards_Are_Remaining_If_Fewer_Than_3_Are_Left()
    {
        var sut = new DrawPileRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Spades, Rank = CardRank.Ace },
            new Card { Suit = Suit.Spades, Rank = CardRank.Two },
        };
        
        var wasRemoved = sut.TryRemoveAt(cardStack, 0, 3, out var transaction);
        
        wasRemoved.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.RemovedCards.ShouldBe(cardStack);
        transaction.UpdatedCardState.ShouldBeEmpty();
    }
}