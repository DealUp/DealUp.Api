namespace DealUp.Domain.Auth.Interfaces;

public interface IAuthService
{
    Task<JwtToken> GetTokenAsync(Credentials credentials);
}