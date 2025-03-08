namespace DealUp.Domain.Auth;

public record JwtToken(string Type, string AccessToken, int ExpiresIn);