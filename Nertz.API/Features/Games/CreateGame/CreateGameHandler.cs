using FastEndpoints;
using ErrorOr;
using Microsoft.Extensions.Options;
using Nertz.API.Features.Games.Shared;
using Nertz.API.Shared.Events;
using Nertz.API.Shared.Factories;
using Nertz.API.Shared.Interfaces;
using Nertz.Domain.Cards;
using Nertz.Infrastructure.Contracts;

namespace Nertz.API.Features.Games;

public class CreateGameHandler : ICommandHandler<CreateGameCommand, ErrorOr<CreateGameResponse>>
{
    private readonly GameSetupOptions _setupOptions;
    private readonly IFactory<CardStack, CardStackType, Card> _cardStackFactory;
    private readonly IShuffle _shuffler;
    private readonly INertzRepository _repository;

    public CreateGameHandler(
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

    public async Task<ErrorOr<CreateGameResponse>> ExecuteAsync(CreateGameCommand command, CancellationToken cancelToken = default)
    {
        var gameResult = Game.CreateGame(_setupOptions, _cardStackFactory, _shuffler, command.TargetScore, command.MaxPlayerCount, command.PlayerIds, command.RoomId);

        if (gameResult.IsError)
        {
            throw new NotImplementedException();
        }
        
        var dbResult = await _repository.CreateGame(gameResult.Value.ToDataModel(), cancelToken);

        if (dbResult.IsError)
        {
            throw new NotImplementedException();
        }

        await new GameCreatedEvent { Game = gameResult.Value, RoomId = command.RoomId }.PublishAsync(Mode.WaitForNone, CancellationToken.None);
        return new CreateGameResponse { GameId = dbResult.Value };
    }
}