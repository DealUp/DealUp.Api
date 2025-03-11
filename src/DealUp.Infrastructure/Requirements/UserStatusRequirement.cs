using DealUp.Domain.User;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Infrastructure.Requirements;

public class UserStatusRequirement(UserVerificationStatus[] allowedStatuses) : IAuthorizationRequirement
{
    public UserVerificationStatus[] AllowedStatuses { get; } = allowedStatuses;
}