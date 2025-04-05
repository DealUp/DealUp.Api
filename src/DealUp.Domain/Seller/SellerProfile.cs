using DealUp.Domain.Abstractions;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Domain.Seller;

public class SellerProfile : AuditableEntityBase
{
    public Guid UserId { get; private set; }
    public UserDomain User { get; private init; } = null!;

    private readonly List<AdvertisementDomain> _advertisements = [];
    public IReadOnlyCollection<AdvertisementDomain> Advertisements => _advertisements.AsReadOnly();

    private SellerProfile(Guid userId)
    {
        UserId = userId;
    }

    public static SellerProfile CreateNew(Guid userId)
    {
        return new SellerProfile(userId);
    }
}