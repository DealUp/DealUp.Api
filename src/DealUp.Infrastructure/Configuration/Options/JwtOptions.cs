using DealUp.Constants;

namespace DealUp.Infrastructure.Configuration.Options;

public class JwtOptions
{
    public static string Section = "Jwt";
    
    public string Issuer { get; set; } = JwtConstants.DEFAULT_JWT_KEY_ISSUER_AUDIENCE;
    public string Audience { get; set; } = JwtConstants.DEFAULT_JWT_KEY_ISSUER_AUDIENCE;
    public int MinutesToExpire { get; set; } = 30;
    public required string Secret { get; set; }
}