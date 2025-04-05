namespace DealUp.Domain.Abstractions;

public abstract class AuditableEntityBase : EntityBase
{
    public DateTime CreatedAt { get; protected init; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; protected set; }
}