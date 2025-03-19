namespace DealUp.Domain.Identity.Interfaces;

public interface IAuthService
{
    public Task<JwtToken> RegisterUserAsync(Credentials credentials);
    public Task<JwtToken> GetTokenAsync(Credentials credentials);
    public JwtToken BuildJwt(User.User user);
}