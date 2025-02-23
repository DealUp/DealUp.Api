using System.ComponentModel.DataAnnotations;
using DealUp.Domain.Auth.Interfaces;
using DealUp.Dto.v1.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.Auth;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
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

    [Authorize]
    [HttpGet("test")]
    public ActionResult TestAuth()
    {
        return new JsonResult(new { Message = "Welcome!" });
    }
}