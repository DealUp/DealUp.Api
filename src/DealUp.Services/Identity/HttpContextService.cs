using System.Security.Claims;
using DealUp.Domain.Identity.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DealUp.Services.Identity;

public class HttpContextService(IHttpContextAccessor httpContextAccessor) : IHttpContextService
{
    public Guid GetUserIdOrThrow()
    {
        var currentUser = httpContextAccessor.HttpContext?.User;
        var userIdString = currentUser?.FindFirstValue(ClaimTypes.NameIdentifier);
        if (currentUser is null || userIdString is null)
        {
            throw new InvalidOperationException("Invalid JWT provided.");
        }

        return Guid.Parse(userIdString);
    }
}