namespace DealUp.Domain.Seller.Interfaces;

public interface ISellerRepository
{
    public Task<SellerProfile?> GetSellerProfileAsync(Guid userId);
    public Task<Guid> CreateSellerProfileAsync(SellerProfile sellerProfile);
}