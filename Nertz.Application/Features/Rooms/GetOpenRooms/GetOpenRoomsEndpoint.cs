using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Nertz.Application.Nertz.Features.Rooms.GetOpenRooms;

public class GetOpenRoomsEndpoint : Endpoint<GetOpenRoomsCommand, GetOpenRoomsResponse>
{
    public override void Configure()
    {
        Get("api/rooms");
        AllowAnonymous(); // TODO - remove when auth is in place;
    }

    public override async Task<Results<Ok<GetOpenRoomsResponse>, InternalServerError, ProblemDetails>> HandleAsync(GetOpenRoomsCommand command, CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.InternalServerError();
        }
        
        return TypedResults.Ok(response.Value);
    }
}