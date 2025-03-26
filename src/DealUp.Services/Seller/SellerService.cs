using DealUp.Domain.Seller;
using DealUp.Domain.Seller.Interfaces;
using DealUp.Exceptions;

namespace DealUp.Services.Seller;

public class SellerService(ISellerRepository sellerRepository) : ISellerService
{
    public async Task CreateSellerProfileAsync(Guid userId)
    {
        var sellerProfile = await sellerRepository.GetSellerProfileAsync(userId);
        if (sellerProfile != null)
        {
            throw new EntityAlreadyExistsException(nameof(SellerProfile));
        }

        sellerProfile = SellerProfile.CreateNew(userId);
        await sellerRepository.CreateSellerProfileAsync(sellerProfile);
    }
}