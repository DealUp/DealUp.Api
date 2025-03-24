namespace DealUp.Domain.Identity;

public record JwtToken(string Type, string AccessToken, int ExpiresIn);