namespace DealUp.Domain.Abstractions;

public abstract class EntityBase(Guid? id = null)
{
    public Guid Id { get; protected init; } = id ?? Guid.CreateVersion7();
    public DateTime CreatedAt { get; protected init; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; protected set; }
}