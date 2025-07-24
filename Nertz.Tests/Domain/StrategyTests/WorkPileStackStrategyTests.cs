using Nertz.Domain.Strategies;
using Nertz.Domain.Cards;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class WorkPileStackStrategyTests
{
    [Fact]
    public void Should_Not_Stack_Same_Color_Cards()
    {
        var sut = new WorkPileStackStrategy();
        var bottom = new Card { Suit = Suit.Clubs, Rank = CardRank.King };
        var top = new Card { Suit = Suit.Spades, Rank = CardRank.Queen };

        var wasSuccess = sut.CanStack(bottom, top);

        wasSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Should_Not_Stack_NonSequential_Cards()
    {
        var sut = new WorkPileStackStrategy();
        var bottom = new Card { Suit = Suit.Diamonds, Rank = CardRank.Eight };
        var top = new Card { Suit = Suit.Spades, Rank = CardRank.Six };
        
        var wasSuccess = sut.CanStack(bottom, top);
        
        wasSuccess.ShouldBeFalse();
    }

    [Fact]
    public void Should_Stack_Alternating_Suit_Color_And_Sequential_Cards()
    {
        var sut = new WorkPileStackStrategy();
        var bottom = new Card { Suit = Suit.Spades, Rank = CardRank.Ten };
        var top = new Card { Suit = Suit.Hearts, Rank = CardRank.Nine };
        
        var wasSuccess = sut.CanStack(bottom, top);
        
        wasSuccess.ShouldBeTrue();
    }
}