using DealUp.Domain.User;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Infrastructure.Requirements;

public class UserStatusRequirement(Status[] allowedStatuses) : IAuthorizationRequirement
{
    public Status[] AllowedStatuses { get; } = allowedStatuses;
}