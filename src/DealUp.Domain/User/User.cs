using DealUp.Domain.Abstractions;
using DealUp.Domain.Identity;

namespace DealUp.Domain.User;

public class User(string username, string? password, UserVerificationStatus status) : EntityBase
{
    public string Username { get; private set; } = username;
    public string? Password { get; private set; } = password;
    public UserVerificationStatus Status { get; private set; } = status;

    public bool IsMatchingPassword(Credentials credentials)
    {
        return Password == credentials.Password;
    }

    public void Confirm()
    {
        Status = UserVerificationStatus.Confirmed;
    }

    public static User CreateNew(string username, string? password)
    {
        return new User(username, password, UserVerificationStatus.Unverified);
    }
}

public enum UserVerificationStatus
{
    Unverified = 1,
    Confirmed
}