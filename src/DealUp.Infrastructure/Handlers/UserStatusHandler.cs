using System.Security.Claims;
using DealUp.Domain.User.Interfaces;
using DealUp.Infrastructure.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Infrastructure.Handlers;

public class UserStatusHandler(IUserRepository userRepository) : AuthorizationHandler<UserStatusRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserStatusRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId))
        {
            return;
        }

        var user = await userRepository.GetUserByIdAsync(Guid.Parse(userId));
        if (user is not null && requirement.AllowedStatuses.Contains(user.Status))
        {
            context.Succeed(requirement);
        }
    }
}