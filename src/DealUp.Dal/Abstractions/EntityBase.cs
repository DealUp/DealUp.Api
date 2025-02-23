using System.ComponentModel.DataAnnotations;

namespace DealUp.Dal.Abstractions;

public abstract class EntityBase
{
    [Key]
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedAt { get; set; }
}