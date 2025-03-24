namespace DealUp.Domain.Identity.Interfaces;

public interface ISsoServiceFactory
{
    public ISsoProviderService GetSsoProvider(string providerName);
}