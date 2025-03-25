namespace DealUp.Domain.Abstractions;

public abstract class EntityBase(Guid? id = null)
{
    public Guid Id { get; private set; } = id ?? Guid.CreateVersion7();
    public DateTime CreatedAt { get; private set; }
    public DateTime? ModifiedAt { get; private set; }
}