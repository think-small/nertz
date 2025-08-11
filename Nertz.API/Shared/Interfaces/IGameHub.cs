namespace Nertz.API.Shared.Interfaces;

public interface IGameHub
{
    Task SendGameUpdates(string testMessage);
}