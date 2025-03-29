namespace DealUp.Domain.Abstractions;

public abstract class EntityBase
{
    public Guid Id { get; protected init; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; protected init; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; protected set; }
}