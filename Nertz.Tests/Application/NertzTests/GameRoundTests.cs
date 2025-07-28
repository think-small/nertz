using Nertz.Application.Shared.Factories;
using Nertz.Application.Nertz;
using Nertz.Application.Shared.Interfaces;
using Nertz.Application.Shared.Errors;
using Nertz.Domain.Cards;
using NSubstitute;
using Shouldly;

namespace Nertz.Tests.Application.NertzTests;

public class GameRoundTests
{
    private readonly IFactory<CardStack, CardStackType, Card> _mockCardStackFactory;
    private readonly Dictionary<int, int> _mockPlayerScores;

    public GameRoundTests()
    {
        _mockCardStackFactory = Substitute.For<IFactory<CardStack, CardStackType, Card>>();
        _mockPlayerScores = new Dictionary<int, int>()
        {
            { 1, 0 },
            { 2, 0 }
        };
    }
    
    [Fact]
    public void Should_Create_A_New_Game_Round()
    {
        var actual = GameRound.Create(_mockCardStackFactory, 1, 100, _mockPlayerScores);
        
        actual.IsError.ShouldBe(false);
        actual.Value.ShouldNotBeNull();
    }

    [Fact]
    public void Should_Error_If_Unable_To_Init_Common_Piles()
    {
        _mockCardStackFactory.Create(Arg.Any<CardStackType>()).ReturnsForAnyArgs(_ => throw new ArgumentException());
        var actual = GameRound.Create(_mockCardStackFactory, 1, 100, _mockPlayerScores);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.ShouldContain(RoundSetupErrors.UnableToInitializeCommonPile);
    }

    [Fact]
    public void Should_Return_Null_If_No_Player_Has_Reached_Target_Score()
    {
        var actual = GameRound.Create(_mockCardStackFactory, 1, 100, _mockPlayerScores);

        actual.Value.RoundWinnerId.ShouldBeNull();
    }
    
    [Fact]
    public void Should_Return_Player_With_Highest_Score_That_Matches_Or_Exceeds_Target_Score()
    {
        var expectedWinnerId = 3;
        _mockPlayerScores.Add(expectedWinnerId, 100);
        var actual = GameRound.Create(_mockCardStackFactory, 1, 100, _mockPlayerScores);
        
        actual.Value.RoundWinnerId.ShouldBe(expectedWinnerId);
    }
}