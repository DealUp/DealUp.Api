namespace DealUp.Infrastructure.Configuration;

public class OauthOptions
{
    public const string SectionName = "OauthOptions";

    public GoogleProviderOptions GoogleProviderOptions { get; set; } = null!;
}

public class GoogleProviderOptions
{
    public string ClientId { get; set; } = string.Empty;
    public string ClientSecret { get; set; } = string.Empty;
    public string CallbackUrl { get; set; } = "/api/v1/sso-auth/callback";
    public string LoginUrl { get; set; } = "/api/v1/sso-auth/login";
}