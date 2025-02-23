using DealUp.Domain.Auth;

namespace DealUp.Domain.User;

public class User(Guid? id, string email, string? password) : Entity(id)
{
    public string Email { get; private set; } = email;
    public string? Password { get; private set; } = password;

    public User(string email, string password) : this(Guid.CreateVersion7(), email, password) { }

    public bool IsMatchingPassword(Credentials credentials)
    {
        return Password == credentials.Password;
    }
}