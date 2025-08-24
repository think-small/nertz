using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Nertz.API.Features.Rooms;

public class LeaveRoomEndpoint : Endpoint<LeaveRoomCommand, Results<Accepted, UnprocessableEntity, ProblemDetails>>
{
    public override void Configure()
    {
        Delete("/api/rooms/players");
        AllowAnonymous(); // TODO - remove when auth is in place
    }

    public override async Task<Results<Accepted, UnprocessableEntity, ProblemDetails>> HandleAsync(
        LeaveRoomCommand command, CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.UnprocessableEntity();
        }
        
        return TypedResults.Accepted(string.Empty);
    }
}