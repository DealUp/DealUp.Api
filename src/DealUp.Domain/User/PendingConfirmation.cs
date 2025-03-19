namespace DealUp.Domain.User;

public class PendingConfirmation(Guid id, Guid userId, ConfirmationType type, string token, bool isUsed, DateTime creationDate) : Entity(id)
{
    public Guid UserId { get; private set; } = userId;
    public ConfirmationType Type { get; private set; } = type;
    public string Token { get; private set; } = token;
    public bool IsUsed { get; private set; } = isUsed;
    public DateTime CreationDate { get; private set; } = creationDate;

    private PendingConfirmation(Guid userId, ConfirmationType type, string token)
        : this(Guid.CreateVersion7(), userId, type, token, isUsed: false, DateTime.UtcNow)
    {
        
    }

    public bool IsRecent()
    {
        TimeSpan difference = DateTime.UtcNow - CreationDate;
        return difference.TotalMinutes <= 5;
    }

    public void SetAsUsed()
    {
        IsUsed = true;
    }

    public static PendingConfirmation CreateForEmailVerification(Guid userId, string token)
    {
        return new PendingConfirmation(userId, ConfirmationType.VerifyEmail, token);
    }
}

public enum ConfirmationType
{
    VerifyEmail = 1,
    ResetPassword
}