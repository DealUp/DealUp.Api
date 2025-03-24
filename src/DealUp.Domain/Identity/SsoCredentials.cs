namespace DealUp.Domain.Identity;

public class SsoCredentials
{
    public string Id { get; private set; }
    public string? Username { get; private set; }
    public string? FullName { get; private set; }

    private SsoCredentials(string id, string? username, string? fullName)
    {
        Id = id;
        Username = username;
        FullName = fullName;
    }

    public static SsoCredentials Create(string id, string? username, string? fullName)
    {
        return new SsoCredentials(id, username, fullName);
    }
}