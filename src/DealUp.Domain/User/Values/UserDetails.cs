namespace DealUp.Domain.User.Values;

public record UserDetails
{
    public string Username { get; private set; }
    public UserVerificationStatus Status { get; private set; }
    public bool HasSellerProfile { get; private set; }

    private UserDetails(string username, UserVerificationStatus status, bool hasSellerProfile)
    {
        Username = username;
        Status = status;
        HasSellerProfile = hasSellerProfile;
    }

    public static UserDetails Create(User user)
    {
        return new UserDetails(user.Username, user.Status, user.HasSellerProfile());
    }
}