using System.ComponentModel.DataAnnotations;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Dto.v1.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.Identity;

[ApiController]
[Route("api/v1/auth")]
public class AuthController(IAuthService authService, IHttpContextService httpContextService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult<JwtTokenDto>> Register([FromBody, Required] CredentialsDto credentialsDto)
    {
        var token = await authService.RegisterUserAsync(credentialsDto.ToDomain());
        return Ok(token.ToDto());
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<JwtTokenDto>> Login([FromBody, Required] CredentialsDto credentialsDto)
    {
        var token = await authService.GetTokenAsync(credentialsDto.ToDomain());
        return Ok(token.ToDto());
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("test")]
    public ActionResult TestAuth()
    {
        var userId = httpContextService.GetUserIdOrThrow();
        return new JsonResult(new { Message = "Welcome!", UserId = userId });
    }
}