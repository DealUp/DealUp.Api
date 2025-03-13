using DealUp.Dal.Abstractions;

namespace DealUp.Dal;

public class UserPendingConfirmation : EntityBase
{
    public required Guid UserId { get; set; }
    public User? User { get; set; }

    public required string Type { get; set; }
    public required string Token { get; set; }
    public required bool IsUsed { get; set; }
}