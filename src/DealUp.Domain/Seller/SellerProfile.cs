using DealUp.Domain.Abstractions;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;
using UserDomain = DealUp.Domain.User.User;

namespace DealUp.Domain.Seller;

public class SellerProfile : EntityBase
{
    public UserDomain User { get; private init; } = null!;

    private readonly List<AdvertisementDomain> _advertisements = [];
    public IReadOnlyCollection<AdvertisementDomain> Advertisements => _advertisements.AsReadOnly();

    private SellerProfile()
    {
        
    }

    public static SellerProfile CreateNew(UserDomain user)
    {
        return new SellerProfile { User = user };
    }
}