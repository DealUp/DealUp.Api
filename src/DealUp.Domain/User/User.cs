namespace DealUp.Domain.User;

public class User(string username, string sha256Password) : Entity
{
    public string Username { get; private set; } = username;
    public string Sha256Password { get; private set; } = sha256Password;
}