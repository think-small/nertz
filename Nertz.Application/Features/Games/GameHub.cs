using Microsoft.AspNetCore.SignalR;
using Nertz.Application.Shared.Interfaces;

namespace Nertz.Application.Nertz.Features.Games;

public class GameHub : Hub<IGameHub>
{
    public async Task JoinGame(int gameId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, $"game-{gameId}");
    }

    public async Task LeaveGame(int gameId)
    {
        await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"game-{gameId}");
    }
    
    public async Task SendGameUpdates(int gameId)
    {
        await Clients.Group($"game-{gameId}").SendGameUpdates("Test Game Update Event");
    }
}