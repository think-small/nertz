using Nertz.API.Features.Players;
using Nertz.API.Shared.Factories;
using Nertz.API.Features.Games.Shared;
using Nertz.API.Shared.Interfaces;
using Nertz.Domain.Strategies;
using Nertz.Domain.Cards;
using Nertz.API.Shared.Errors;
using NSubstitute;
using Shouldly;

namespace Nertz.Tests.Application.NertzTeests;

public class GameTests
{
    private readonly IShuffle _mockShuffle;
    private readonly IFactory<CardStack, CardStackType, Card> _mockCardStackFactory;
    private readonly GameSetupOptions _mockSetupOptions;
    private readonly PlayerHand _mockPlayerHand;
    private readonly int _mockRoomId;
    
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
        _mockRoomId = 1;
    }
    
    [Fact]
    public void Should_Create_Game()
    {
        var mockPlayerIds = new[] { 1, 2 };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds, _mockRoomId);
        
        actual.IsError.ShouldBe(false);
        actual.Value.ShouldBeOfType<Game>();
    }
    
    [Fact]
    public void Should_Error_If_Fewer_Than_2_Players()
    {
        var mockPlayerIds = new[] { 1 };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds, _mockRoomId);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.InsufficientPlayerCount);
    }

    [Fact]
    public void Should_Error_If_Player_Count_Exceeds_Max_Player_Setting()
    {
        var mockPlayerIds = new[] { 1, 2, 3, 4 };
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, _mockSetupOptions.MinTargetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds, _mockRoomId);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.MaximumPlayerCountExceeded);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Should_Error_If_Target_Score_Is_Invalid(int targetScore)
    {
        var mockPlayerIds = new[] { 1, 2, 3};
        var actual = Game.CreateGame(_mockSetupOptions, _mockCardStackFactory, _mockShuffle, targetScore, _mockSetupOptions.MaxPlayerCount, mockPlayerIds, _mockRoomId);
        
        actual.IsError.ShouldBe(true);
        actual.Errors.Count.ShouldBe(1);
        actual.FirstError.ShouldBe(GameSetupErrors.TargetScoreInvalid);
    }
}