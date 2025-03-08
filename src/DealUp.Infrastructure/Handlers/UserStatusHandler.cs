using System.Security.Claims;
using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using DealUp.Infrastructure.Requirements;
using Microsoft.AspNetCore.Authorization;

namespace DealUp.Infrastructure.Handlers;

public class UserStatusHandler(IUserRepository userRepository) : AuthorizationHandler<UserStatusRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserStatusRequirement requirement)
    {
        var userEmail = context.User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
        {
            return;
        }

        var user = await userRepository.GetUserByEmailAsync(userEmail);
        if (user is not null && requirement.AllowedStatuses.Contains(user.Status))
        {
            context.Succeed(requirement);
        }
    }
}