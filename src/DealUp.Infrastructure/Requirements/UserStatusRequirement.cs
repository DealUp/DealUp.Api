using DealUp.Domain.User.Values;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Infrastructure.Requirements;

public class UserStatusRequirement(UserVerificationStatus[] allowedStatuses) : IAuthorizationRequirement
{
    public UserVerificationStatus[] AllowedStatuses { get; } = allowedStatuses;
}