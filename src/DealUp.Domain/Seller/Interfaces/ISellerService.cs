namespace DealUp.Domain.Seller.Interfaces;

public interface ISellerService
{
    public Task CreateSellerProfileAsync(Guid userId);
}