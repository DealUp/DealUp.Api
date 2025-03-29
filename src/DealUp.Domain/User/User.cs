using DealUp.Domain.Abstractions;
using DealUp.Domain.Identity;
using DealUp.Domain.User.Values;

namespace DealUp.Domain.User;

public class User : EntityBase
{
    public string Username { get; private set; }
    public string? Password { get; private set; }
    public UserVerificationStatus Status { get; private set; }

    private User(string username, string? password, UserVerificationStatus status)
    {
        Username = username;
        Password = password;
        Status = status;
    }

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

    public static User Create(string username, string? password, UserVerificationStatus status)
    {
        return new User(username, password, status);
    }
}