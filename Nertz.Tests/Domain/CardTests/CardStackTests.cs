using Nertz.Domain.Contracts;
using Nertz.Domain.Cards;
using NSubstitute;
using NSubstitute.ClearExtensions;
using Shouldly;

namespace Nertz.Tests.Domain.ValueObjects;

public class CardStackTests
{
    private const int MAX_SIZE = 13;
    private readonly IRemoveStrategy _mockRemoveStrategy;
    private readonly IStackStrategy _mockStackStrategy;
    
    public CardStackTests()
    {
        _mockRemoveStrategy = Substitute.For<IRemoveStrategy>();
        _mockStackStrategy = Substitute.For<IStackStrategy>();
        _mockStackStrategy.CanStack(Arg.Any<Card>(), Arg.Any<Card>()).Returns(true);
    }
    
    [Fact]
    public void Should_Add_Card_To_Stack_With_Existing_Cards()
    {
        var bottom = new Card { Suit = Suit.Spades, Rank = CardRank.Five };
        var top = new Card { Suit = Suit.Spades, Rank = CardRank.Six };
        var sut = new CardStack(_mockStackStrategy, _mockRemoveStrategy, MAX_SIZE, [bottom]);
        
        var wasSuccess = sut.AddToStack(top);

        wasSuccess.ShouldBeTrue();
        sut.Size.ShouldBe(2);
    }

    [Fact]
    public void Should_Add_Card_To_Empty_Stack()
    {
        var top = new Card { Suit = Suit.Hearts, Rank = CardRank.Two };
        var sut = new CardStack(_mockStackStrategy, _mockRemoveStrategy, MAX_SIZE);
        
        var wasSuccess = sut.AddToStack(top);
        
        wasSuccess.ShouldBeTrue();
        sut.Size.ShouldBe(1);
    }

    [Fact]
    public void Should_Not_Increase_Size_Of_Stack_If_Card_Is_Not_Added()
    {
        var bottom = new Card { Suit = Suit.Spades, Rank = CardRank.Five };
        var top = new Card { Suit = Suit.Hearts, Rank = CardRank.Two };
        _mockStackStrategy.CanStack(bottom, top).Returns(false);
        var sut = new CardStack(_mockStackStrategy, _mockRemoveStrategy, MAX_SIZE, [bottom]);
        
        var wasSuccess = sut.AddToStack(top);
        
        wasSuccess.ShouldBeFalse();
        sut.Size.ShouldBe(1);
    }

    [Fact]
    public void Should_Not_Change_Stack_Or_Return_Anything_If_Removal_Fails()
    {
        _mockRemoveStrategy.TryRemoveAt([], 0, 0, out Arg.Any<CardTransaction>()).Returns(false);
        var sut = new CardStack(_mockStackStrategy, _mockRemoveStrategy, MAX_SIZE);

        var wasSuccess = sut.TryRemoveAt(1, 0, out var transaction);
        
        wasSuccess.ShouldBeFalse();
        transaction.ShouldBeNull();
    }

    [Fact]
    public void Should_Return_Removed_Cards_In_A_New_CardStack()
    {
        var mockOriginalCardStack = new[]
        {
            new Card { Suit = Suit.Clubs, Rank = CardRank.Nine },
            new Card { Suit = Suit.Hearts, Rank = CardRank.Seven },
            new Card { Suit = Suit.Diamonds, Rank = CardRank.Ace }
        };
        
        _mockRemoveStrategy.TryRemoveAt([], 0, 0, out Arg.Any<CardTransaction>())
            .ReturnsForAnyArgs(arg =>
            {
                arg[3] = new CardTransaction
                {
                    RemovedCards = new Card[]
                    {
                        new Card { Suit = Suit.Hearts, Rank = CardRank.Seven },
                        new Card { Suit = Suit.Diamonds, Rank = CardRank.Ace }
                    },
                    UpdatedCardState = new Card[]
                    {
                        new Card { Suit = Suit.Clubs, Rank = CardRank.Nine }
                    }
                };
                return true;
            });
        var sut = new CardStack(_mockStackStrategy, _mockRemoveStrategy, MAX_SIZE, mockOriginalCardStack);
        
        var wasSuccess = sut.TryRemoveAt(1, 2, out var transaction);
        
        wasSuccess.ShouldBeTrue();
        transaction.ShouldNotBeNull();
        transaction.Size.ShouldBe(2);
        sut.Size.ShouldBe(1);
    }
}