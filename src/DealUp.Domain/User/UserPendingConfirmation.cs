using DealUp.Domain.Abstractions;
using DealUp.Domain.User.Values;

namespace DealUp.Domain.User;

public class UserPendingConfirmation : EntityBase
{
    public User User { get; private init; } = null!;
    public ConfirmationType Type { get; private set; }
    public string Token { get; private set; }
    public bool IsUsed { get; private set; }

    private UserPendingConfirmation(ConfirmationType type, string token, bool isUsed)
    {
        Type = type;
        Token = token;
        IsUsed = isUsed;
    }

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
        return new UserPendingConfirmation(ConfirmationType.VerifyEmail, token, isUsed: false)
        {
            User = user
        };
    }
}