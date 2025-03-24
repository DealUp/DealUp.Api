namespace DealUp.Domain.Identity.Interfaces;

public interface IHttpContextService
{
    public Guid GetUserIdOrThrow();
    public Task<SsoCredentials> AuthenticateAsync(string authenticationScheme);
    public Task SignOutAsync(string authenticationScheme);
}
