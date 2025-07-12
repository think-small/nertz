using Nertz.Application.Factories;
using Nertz.Application.Nertz;
using Nertz.Application.Nertz.Shared.Interfaces;
using Nertz.Application.Players;
using Nertz.Application.Shared.Errors;
using Nertz.Domain.Strategies;
using Nertz.Domain.ValueObjects;
using NSubstitute;
using Shouldly;

namespace Nertz.Tests.Application.NertzTeests;

public class GameTests
{
    private readonly IShuffle _mockShuffle;
    private readonly IFactory<CardStack, CardStackType, Card> _mockCardStackFactory;
    private readonly GameSetupOptions _mockSetupOptions;
    private readonly PlayerHand _mockPlayerHand;
    
    public GameTests()
    {
        _mockSetupOptions = new GameSetupOptions()
        {
            MaxPlayerCount = 3,
            MinPlayerCount = 2,
            MaxTargetScore = 100,
            MinTargetScore = 1,
            MaxFanSize = 13,
            MaxDrawPileSize = 13,
        };
        _mockPlayerHand = new PlayerHand
        {
            Fan = new CardStack(new NoStackStrategy(), new NoRemoveStrategy(), _mockSetupOptions.MaxFanSize),
            DrawPile = new CardStack(new NoStackStrategy(), new NoRemoveStrategy(), _mockSetupOptions.MaxDrawPileSize),
            WorkPiles = []
        };
        _mockShuffle = Substitute.For<IShuffle>();
        _mockCardStackFactory = Substitute.For<IFactory<CardStack, CardStackType, Card>>();
    }
    
    [Fact]
    public void Should_Create_Game()
    {
        var mockPlayerIds = new[] { Guid.NewGuid(), Guid.NewGuid() };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds);
        
        actual.IsError.ShouldBe(false);
        actual.Value.ShouldBeOfType<Game>();
    }
    
    [Fact]
    public void Should_Error_If_Fewer_Than_2_Players()
    {
        var mockPlayerIds = new[] { Guid.NewGuid() };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.InsufficientPlayerCount);
    }

    [Fact]
    public void Should_Error_If_Player_Count_Exceeds_Max_Player_Setting()
    {
        var mockPlayerIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.MaximumPlayerCountExceeded);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Should_Error_If_Target_Score_Is_Invalid(int targetScore)
    {
        var mockPlayerIds = new[] { Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid() };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, targetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.TargetScoreInvalid);
    }
}