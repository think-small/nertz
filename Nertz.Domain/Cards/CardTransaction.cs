namespace Nertz.Domain.Cards;

public sealed record CardTransaction
{
    public required Card[] RemovedCards { get; init; }
    public required Card[] UpdatedCardState { get; init; }
}