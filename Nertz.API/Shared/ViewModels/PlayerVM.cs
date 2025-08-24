namespace Nertz.API.Shared.ViewModels;

public class PlayerVM
{
    public required string UserName { get; init; }
    public bool IsHost { get; init; }
}