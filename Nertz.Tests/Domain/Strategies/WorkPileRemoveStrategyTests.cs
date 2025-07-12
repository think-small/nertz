using Nertz.Domain.Strategies;
using Nertz.Domain.ValueObjects;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class WorkPileRemoveStrategyTests
{
    [Fact]
    public void Should_Not_Remove_If_Out_Of_Bounds()
    {
        var sut = new WorkPileRemoveStrategy();

        var wasSuccess = sut.TryRemoveAt([], 1, 1, out var transaction);

        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Remove_If_Attempting_To_Overdraw()
    {
        var sut = new WorkPileRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Clubs, Rank = CardRank.King }
        };
        
        var wasSuccess = sut.TryRemoveAt(cardStack, 0, 2, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Not_Remove_If_Not_Slicing_To_End_Of_Stack()
    {
        var sut = new WorkPileRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Clubs, Rank = CardRank.King },
            new Card { Suit = Suit.Clubs, Rank = CardRank.Queen },
            new Card { Suit = Suit.Clubs, Rank = CardRank.Jack }
        };
        
        var wasSuccess = sut.TryRemoveAt(cardStack, 1, 1, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Remove_Cards_To_End_Of_Stack()
    {
        var sut = new WorkPileRemoveStrategy();
        var cardStack = new Card[]
        {
            new Card { Suit = Suit.Clubs, Rank = CardRank.King },
            new Card { Suit = Suit.Clubs, Rank = CardRank.Queen },
            new Card { Suit = Suit.Clubs, Rank = CardRank.Jack },
            new Card { Suit = Suit.Clubs, Rank = CardRank.Ten }
        };
        
        var wasSuccess = sut.TryRemoveAt(cardStack, 1, 3, out var transaction);
        
        wasSuccess.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.RemovedCards.ShouldBe(cardStack.Skip(1).Take(3));
        transaction.UpdatedCardState.ShouldBe([cardStack.First()]);
    }
}