using System.Security.Claims;
using DealUp.Domain.Identity;
using DealUp.Domain.Identity.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace DealUp.Services.Identity;

public class HttpContextService(IHttpContextAccessor httpContextAccessor) : IHttpContextService
{
    public Guid GetUserIdOrThrow()
    {
        var currentUser = httpContextAccessor.HttpContext!.User;
        var userIdString = currentUser.FindFirstValue(ClaimTypes.NameIdentifier);

        if (currentUser is null || userIdString is null)
        {
            throw new InvalidOperationException("Invalid JWT provided.");
        }

        return Guid.Parse(userIdString);
    }

    public async Task<SsoCredentials> AuthenticateAsync(string authenticationScheme)
    {
        var currentUser = await httpContextAccessor.HttpContext!.AuthenticateAsync(authenticationScheme);

        var id = currentUser.Principal!.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var username = currentUser.Principal.FindFirst(ClaimTypes.Email)?.Value;
        var fullName = currentUser.Principal.FindFirst(ClaimTypes.Name)?.Value;

        return SsoCredentials.Create(id, username, fullName);
    }

    public Task SignOutAsync(string authenticationScheme)
    {
        return httpContextAccessor.HttpContext!.SignOutAsync(authenticationScheme);
    }
}