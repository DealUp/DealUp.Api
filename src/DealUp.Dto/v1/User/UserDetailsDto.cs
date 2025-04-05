using DealUp.Domain.User.Values;

namespace DealUp.Dto.v1.User;

public class UserDetailsDto
{
    public required string Username { get; set; }
    public required UserVerificationStatus Status { get; set; }
    public required bool HasSellerProfile { get; set; }
}