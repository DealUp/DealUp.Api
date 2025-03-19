using DealUp.Domain.Identity;

namespace DealUp.Domain.User;

public class User(Guid? id, string username, string? password, UserVerificationStatus status) : Entity(id)
{
    public string Username { get; private set; } = username;
    public string? Password { get; private set; } = password;
    public UserVerificationStatus Status { get; private set; } = status;

    private User(string username, string? password, UserVerificationStatus status) : this(Guid.CreateVersion7(), username, password, status)
    {
        
    }

    public bool IsMatchingPassword(Credentials credentials)
    {
        return Password == credentials.Password;
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