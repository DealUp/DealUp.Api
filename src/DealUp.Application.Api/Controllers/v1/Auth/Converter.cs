using DealUp.Domain.Auth;
using DealUp.Dto.v1.Auth;
using DealUp.Utils;

namespace DealUp.Application.Api.Controllers.v1.Auth;

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
}