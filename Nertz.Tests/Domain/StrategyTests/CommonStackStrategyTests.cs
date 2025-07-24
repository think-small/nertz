using Nertz.Domain.Strategies;
using Nertz.Domain.Cards;
using Shouldly;

namespace Nertz.Tests.Domain.Strategies;

public class CommonStackStrategyTests
{
    [Fact]
    public void Should_Not_Stack_When_Different_Suit()
    {
        var sut = new CommonStackStrategy();
        var bottom = new Card { Suit = Suit.Clubs, Rank = CardRank.Two };
        var top = new Card { Suit = Suit.Diamonds, Rank = CardRank.Three };
        
        var actual = sut.CanStack(bottom, top);
        actual.ShouldBeFalse();
    }

    [Fact]
    public void Should_Not_Stack_When_Ranks_Are_Not_Sequential()
    {
        var sut = new CommonStackStrategy();
        var bottom = new Card { Suit = Suit.Clubs, Rank = CardRank.Two };
        var top = new Card { Suit = Suit.Clubs, Rank = CardRank.King };
        
        var actual = sut.CanStack(bottom, top);
        actual.ShouldBeFalse();
    }
    
    [Fact]
    public void Should_Stack_When_Suits_Match_And_Ranks_Are_Sequential()
    {
        var sut = new CommonStackStrategy();
        var bottom = new Card { Suit = Suit.Clubs, Rank = CardRank.Two };
        var top = new Card { Suit = Suit.Clubs, Rank = CardRank.Three };
        
        var actual = sut.CanStack(bottom, top);
        actual.ShouldBeTrue();
    }
}