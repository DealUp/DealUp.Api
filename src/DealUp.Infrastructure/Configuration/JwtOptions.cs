using DealUp.Constants;

namespace DealUp.Infrastructure.Configuration;

public class JwtOptions
{
    public const string SectionName = "JwtOptions";

    public string Issuer { get; set; } = JwtConstants.DefaultJwtKeyIssuerAudience;
    public string Audience { get; set; } = JwtConstants.DefaultJwtKeyIssuerAudience;
    public int MinutesToExpire { get; set; } = 30;
    public required string Secret { get; set; }
}