using System.ComponentModel.DataAnnotations;
using DealUp.Domain.Identity.Interfaces;
using DealUp.Dto.v1.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DealUp.Application.Api.Controllers.v1.Identity;

[ApiController]
[AllowAnonymous]
[Route("api/v1/sso-auth")]
public class SsoAuthController(ISsoServiceFactory ssoServiceFactory) : ControllerBase
{
    [HttpGet("{providerName:required}")]
    public IActionResult StartChallenge([FromRoute] string providerName)
    {
        var providerService = ssoServiceFactory.GetSsoProvider(providerName);
        var properties = providerService.GetAuthenticationProperties();
        return Challenge(properties.ToAuthenticationProperties(), properties.Scheme);
    }

    [HttpGet("login")]
    public async Task<ActionResult<JwtTokenDto>> LoginViaSsoProvider([FromQuery, Required] SsoProviderDto ssoProvider)
    {
        var providerService = ssoServiceFactory.GetSsoProvider(ssoProvider.Name);
        var jwt = await providerService.LoginViaSsoProviderAsync();
        return Ok(jwt.ToDto());
    }
}