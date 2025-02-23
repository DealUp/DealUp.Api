namespace DealUp.Domain;

public abstract class Entity(Guid? id = null)
{
    public Guid Id { get; private set; } = id ?? Guid.CreateVersion7();
}