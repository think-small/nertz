using FastEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using ProblemDetails = FastEndpoints.ProblemDetails;

namespace Nertz.API.Features.Rooms;

[FastEndpoints.HttpGet("api/rooms")]
[AllowAnonymous]
public class GetOpenRoomsEndpoint : Endpoint<GetOpenRoomsCommand, Results<Ok<GetOpenRoomsResponse>, InternalServerError, ProblemDetails>>
{
    public override async Task<Results<Ok<GetOpenRoomsResponse>, InternalServerError, ProblemDetails>> ExecuteAsync(GetOpenRoomsCommand command, CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.InternalServerError();
        }
        
        return TypedResults.Ok(response.Value);
    }
}