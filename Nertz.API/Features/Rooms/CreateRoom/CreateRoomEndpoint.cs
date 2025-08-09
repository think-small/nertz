using FastEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Created = Microsoft.AspNetCore.Http.HttpResults.Created;

namespace Nertz.Application.Nertz.Features.Rooms.CreateRoom;

public class CreateRoomEndpoint : Endpoint<CreateRoomCommand, CreateRoomResponse>
{
    public override void Configure()
    {
        Post("api/rooms");
        AllowAnonymous(); // TODO - remove when auth is in place;
    }

    public override async Task<Results<Created, UnprocessableEntity, ProblemDetails>> HandleAsync(CreateRoomCommand command, CancellationToken cancelToken)
    {
        var response = await command.ExecuteAsync(cancelToken);

        if (response.IsError)
        {
            return TypedResults.UnprocessableEntity();
        }
        
        return TypedResults.Created();
    }
}