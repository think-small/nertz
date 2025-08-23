using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Nertz.Application.Features.Rooms;

public class JoinRoomEndpoint : Endpoint<JoinRoomCommand, JoinRoomResponse>
{
    public override void Configure()
    {
        Post("/api/rooms/players");
        AllowAnonymous(); // TODO - remove when auth is in place;
    }

    public override async Task<Results<Accepted, UnprocessableEntity, ProblemDetails>> HandleAsync(JoinRoomCommand command,
        CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.UnprocessableEntity();
        }
        
        return TypedResults.Accepted(string.Empty);
    }
}