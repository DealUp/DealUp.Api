using DealUp.Domain.Auth;
using DealUp.Dto.v1.Auth;
using DealUp.Utils;

namespace DealUp.Application.Api.Controllers.v1;

public static class DtoConverter
{
    public static JwtTokenDto ToDto(this JwtToken token)
    {
        return new JwtTokenDto
        {
            AccessToken = token.Value
        };
    }

    public static Credentials ToDomain(this CredentialsDto credentialsDto)
    {
        return new Credentials(credentialsDto.Username, credentialsDto.Password.ToSha256());
    }
}