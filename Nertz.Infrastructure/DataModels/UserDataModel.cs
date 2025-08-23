namespace Nertz.Infrastructure.DataModels;

public class UserDataModel
{
    public int Id { get; init; }
    public required string UserName { get; init; }
    public required string Email { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
    public DateTimeOffset UpdatedAt { get; init; }
}