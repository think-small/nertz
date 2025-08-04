namespace Nertz.Application.Shared.Interfaces;

public interface IGameHub
{
    Task SendGameUpdates(string testMessage);
}