using DealUp.Domain.Identity.Interfaces;
using DealUp.Domain.User;
using DealUp.Domain.User.Interfaces;
using DealUp.Dto.v1.User;
using DealUp.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.User;

[ApiController]
[Route("api/v1/user")]
public class UserController(IHttpContextService httpContextService, IUserService userService) : ControllerBase
{
    [Authorize]
    [AuthorizeFor(UserVerificationStatus.Unverified)]
    [HttpGet("start-email-verification")]
    public async Task<ActionResult<StartVerificationResponseDto>> StartEmailVerification()
    {
        var userId = httpContextService.GetUserIdOrThrow();
        var verificationResponse = await userService.SendVerificationEmailIfNeededAsync(userId);
        return Ok(verificationResponse.ToDto());
    }

    [AllowAnonymous]
    [HttpGet("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromQuery] Guid userId, [FromQuery] string token)
    {
        await userService.VerifyUserAsync(userId, token);
        return Ok();
    }
}