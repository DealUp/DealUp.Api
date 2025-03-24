using DealUp.Domain.Identity;
using DealUp.Dto.v1.Identity;
using DealUp.Utils;
using Microsoft.AspNetCore.Authentication;

namespace DealUp.Application.Api.Controllers.v1.Identity;

public static class Converter
{
    public static JwtTokenDto ToDto(this JwtToken token)
    {
        return new JwtTokenDto
        {
            Type = token.Type,
            AccessToken = token.AccessToken,
            ExpiresIn = token.ExpiresIn
        };
    }

    public static Credentials ToDomain(this CredentialsDto credentialsDto)
    {
        return new Credentials(credentialsDto.Username, credentialsDto.Password.ToSha256());
    }

    public static AuthenticationProperties ToAuthenticationProperties(this SsoAuthenticationProperties properties)
    {
        return new AuthenticationProperties
        {
            RedirectUri = properties.RedirectUri,
            Items =
            {
                { "scheme", properties.Scheme },
                { "returnUrl", properties.RedirectUri }
            }
        };
    }
}