using DealUp.Domain.User;
using DealUp.Infrastructure.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DealUp.Infrastructure.Filters;

public class UserStatusAuthorizationFilter(IAuthorizationService authorizationService, Status[] allowedStatuses) : IAsyncAuthorizationFilter
{
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var userStatusRequirement = new UserStatusRequirement(allowedStatuses);
        var result = await authorizationService.AuthorizeAsync(context.HttpContext.User, null, userStatusRequirement);

        if (!result.Succeeded)
        {
            context.Result = new ForbidResult();
        }
    }
}