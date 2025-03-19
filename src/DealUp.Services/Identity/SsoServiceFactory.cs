using DealUp.Domain.Identity.Interfaces;

namespace DealUp.Services.Identity;

public class SsoServiceFactory(IEnumerable<ISsoProviderService> ssoProviders) : ISsoServiceFactory
{
    public ISsoProviderService GetSsoProvider(string providerName)
    {
        return ssoProviders.FirstOrDefault(service => service.GetType().Name == GetServiceName(providerName))
            ?? throw new ArgumentOutOfRangeException(nameof(providerName));
    }

    private static string GetServiceName(string providerName)
    {
        return $"{providerName}SsoService";
    }
}