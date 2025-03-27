namespace DealUp.Domain.Abstractions;

public abstract class EntityBase
{
    public Guid Id { get; protected init; } = Guid.CreateVersion7();
}