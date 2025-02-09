using DealUp.Domain.Auth.Interfaces;
using DealUp.Dto.v1.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<JwtTokenDto>> Login([FromBody]CredentialsDto credentialsDto)
    {
        var token = await authService.GetTokenAsync(credentialsDto.ToDomain());
        return Ok(token.ToDto());
    }

    [Authorize]
    [HttpGet("test")]
    public ActionResult TestAuth()
    {
        return Ok("Welcome!");
    }
}