namespace DealUp.Domain.Identity.Interfaces;

public interface ISsoProviderService
{
    public string AuthenticationScheme { get; }
    public SsoAuthenticationProperties GetAuthenticationProperties();
    public Task<JwtToken> LoginViaSsoProviderAsync();
}