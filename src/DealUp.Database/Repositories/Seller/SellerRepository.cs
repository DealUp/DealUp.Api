using DealUp.Database.Interfaces;
using DealUp.Domain.Seller;
using DealUp.Domain.Seller.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DealUp.Database.Repositories.Seller;

public class SellerRepository(IDatabaseContext databaseContext) : ISellerRepository
{
    public async Task<SellerProfile?> GetSellerProfileAsync(Guid userId)
    {
        return await databaseContext.Set<SellerProfile>()
            .AsNoTracking()
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.User.Id == userId);
    }

    public async Task<Guid> CreateSellerProfileAsync(SellerProfile sellerProfile)
    {
        await databaseContext.AddAsync(sellerProfile);
        await databaseContext.SaveChangesAsync();
        return sellerProfile.Id;
    }
}