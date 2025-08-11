using FastEndpoints;
using ErrorOr;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using Nertz.API.Features.Games.Shared;
using Nertz.API.Shared.Factories;
using Nertz.API.Shared.Interfaces;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.Contracts;
using Created = Microsoft.AspNetCore.Http.HttpResults.Created;

namespace Nertz.API.Features.Games;

public class CreateGameEndpoint : Endpoint<CreateGameCommand, ErrorOr<CreateGameResponse>>
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

    public override async Task<Results<Created, UnprocessableEntity, ProblemDetails>> HandleAsync(CreateGameCommand command, CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.UnprocessableEntity();
        }
        
        return TypedResults.Created();
    }
}