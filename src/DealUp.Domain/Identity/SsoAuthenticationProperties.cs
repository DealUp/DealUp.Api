namespace DealUp.Domain.Identity;

public class SsoAuthenticationProperties
{
    public string RedirectUri { get; private set; }
    public string Scheme { get; private set; }

    private SsoAuthenticationProperties(string redirectUri, string scheme)
    {
        RedirectUri = redirectUri;
        Scheme = scheme;
    }

    public static SsoAuthenticationProperties Create(string redirectUri, string scheme)
    {
        return new SsoAuthenticationProperties(redirectUri, scheme);
    }
}