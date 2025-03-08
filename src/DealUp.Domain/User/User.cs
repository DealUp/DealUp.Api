using DealUp.Domain.Auth;

namespace DealUp.Domain.User;

public class User(Guid? id, string email, string? password, Status status) : Entity(id)
{
    public string Email { get; private set; } = email;
    public string? Password { get; private set; } = password;
    public Status Status { get; private set; } = status;

    private User(string email, string password, Status status) : this(Guid.CreateVersion7(), email, password, status) { }

    public bool IsMatchingPassword(Credentials credentials)
    {
        return Password == credentials.Password;
    }

    public static User CreateNew(string email, string password)
    {
        return new User(email, password, Status.Unverified);
    }
}

public enum Status
{
    Unverified = 1,
    Confirmed
}