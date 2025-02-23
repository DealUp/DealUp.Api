using System.ComponentModel.DataAnnotations;

namespace DealUp.Dal.Abstractions;

public abstract class EntityBase
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
}