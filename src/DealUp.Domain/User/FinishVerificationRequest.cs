namespace DealUp.Domain.User;

public class FinishVerificationRequest
{
    public Guid UserId { get; private set; }
    public string Token { get; private set; }

    private FinishVerificationRequest(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
    }

    public static FinishVerificationRequest Create(Guid userId, string token)
    {
        return new FinishVerificationRequest(userId, token);
    }
}