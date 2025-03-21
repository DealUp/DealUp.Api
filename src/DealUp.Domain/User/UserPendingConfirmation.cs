using DealUp.Domain.Abstractions;

namespace DealUp.Domain.User;

public class UserPendingConfirmation(User user, ConfirmationType type, string token, bool isUsed) : EntityBase
{
    public User User { get; private init; } = user;
    public ConfirmationType Type { get; private set; } = type;
    public string Token { get; private set; } = token;
    public bool IsUsed { get; private set; } = isUsed;

    public bool IsRecent()
    {
        TimeSpan difference = DateTime.UtcNow - CreatedAt;
        return difference.TotalMinutes <= 5;
    }

    public void SetAsUsed()
    {
        IsUsed = true;
    }

    public void ConfirmUser()
    {
        IsUsed = true;
        User.Confirm();
    }

    public static UserPendingConfirmation CreateForEmailVerification(User user, string token)
    {
        return new UserPendingConfirmation(user, ConfirmationType.VerifyEmail, token, isUsed: false);
    }
}

public enum ConfirmationType
{
    VerifyEmail = 1,
    ResetPassword
}