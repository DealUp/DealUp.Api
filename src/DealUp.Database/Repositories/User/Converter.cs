using DealUp.Dal;
using DealUp.Domain.User;
using DealUp.Utils;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Database.Repositories.User;

public static class Converter
{
    public static UserDomain ToDomain(this Dal.User user)
    {
        return new UserDomain(user.Id, user.Email, user.Password, user.Status.ToEnum<Status>());
    }

    public static Dal.User ToDal(this UserDomain user)
    {
        return new Dal.User
        {
            Id = user.Id,
            Email = user.Email,
            Password = user.Password,
            Status = user.Status.ToString()
        };
    }

    public static PendingConfirmation ToDomain(this UserPendingConfirmation pendingConfirmation)
    {
        return new PendingConfirmation(
            pendingConfirmation.Id,
            pendingConfirmation.UserId,
            pendingConfirmation.Type.ToEnum<ConfirmationType>(),
            pendingConfirmation.Token,
            pendingConfirmation.IsUsed,
            pendingConfirmation.CreatedAt);
    }

    public static UserPendingConfirmation ToDal(this PendingConfirmation pendingConfirmation)
    {
        return new UserPendingConfirmation
        {
            Id = pendingConfirmation.Id,
            UserId = pendingConfirmation.UserId,
            Type = pendingConfirmation.Type.ToString(),
            Token = pendingConfirmation.Token,
            IsUsed = pendingConfirmation.IsUsed,
            CreatedAt = pendingConfirmation.CreationDate
        };
    }
}