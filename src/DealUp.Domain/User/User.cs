using DealUp.Domain.Abstractions;
using DealUp.Domain.Identity;
using DealUp.Domain.Seller;
using DealUp.Domain.User.Values;

namespace DealUp.Domain.User;

public class User : AuditableEntityBase
{
    public string Username { get; private set; }
    public string? Password { get; private set; }
    public UserVerificationStatus Status { get; private set; }
    public SellerProfile? SellerProfile { get; private init; }

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

    public bool HasSellerProfile()
    {
        return SellerProfile is not null;
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