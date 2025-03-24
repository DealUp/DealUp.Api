namespace DealUp.Dto.v1.Identity;

public class JwtTokenDto
{
    public required string Type { get; set; }
    public required string AccessToken { get; set; }
    public required int ExpiresIn { get; set; }
}