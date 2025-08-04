using FastEndpoints;
using ErrorOr;
using Microsoft.Extensions.Options;
using Nertz.Application.Shared.Factories;
using Nertz.Application.Shared.Interfaces;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.Contracts;

namespace Nertz.Application.Nertz.Features.CreateGame;

public class CreateGameEndpoint : Endpoint<CreateGameRequest, CreateGameResponse>
{
    private readonly GameSetupOptions _setupOptions;
    private readonly IFactory<CardStack, CardStackType, Card> _cardStackFactory;
    private readonly IShuffle _shuffler;
    private readonly INertzRepository _repository;

    public CreateGameEndpoint(
        IOptions<GameSetupOptions> setupOptions,
        IFactory<CardStack, CardStackType, Card> cardStackFactory,
        IShuffle shuffler,
        INertzRepository repository)
    {
        _setupOptions = setupOptions.Value;
        _cardStackFactory = cardStackFactory;
        _shuffler = shuffler;
        _repository = repository;
    }
    
    public override void Configure()
    {
        Post("api/nertz/game");
        AllowAnonymous(); // TODO - remove when auth is in place
    }

    public override async Task<ErrorOr<int>> HandleAsync(CreateGameRequest req, CancellationToken cancelToken)
    {
        var gameResult = Game.CreateGame(_setupOptions, _cardStackFactory, _shuffler, req.TargetScore, req.MaxPlayerCount, req.PlayerIds);

        if (gameResult.IsError)
        {
            throw new NotImplementedException();
        }
        
        var dbResult = await _repository.CreateGame(gameResult.Value.ToDataModel(), cancelToken);

        if (dbResult.IsError)
        {
            throw new NotImplementedException();
        }
        
        return dbResult.Value;
    }
}