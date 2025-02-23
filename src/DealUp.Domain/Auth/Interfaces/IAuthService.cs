namespace DealUp.Domain.Auth.Interfaces;

public interface IAuthService
{
    public Task<JwtToken> RegisterUserAsync(Credentials credentials);
    public Task<JwtToken> GetTokenAsync(Credentials credentials);
}