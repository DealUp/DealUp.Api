using DealUp.Database.Extensions;
using DealUp.Database.Interfaces;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;

namespace DealUp.Database.Repositories.Advertisement;

public class AdvertisementRepository(IDatabaseContext databaseContext) : IAdvertisementRepository
{
    public async Task<AdvertisementDomain> CreateAdvertisementAsync(AdvertisementDomain advertisement)
    {
        await databaseContext.AddAsync(advertisement);
        await databaseContext.SaveChangesAsync();
        return advertisement;
    }

    public async Task<PagedResponse<AdvertisementDomain>> GetAllAdvertisementsAsync(PaginationParameters pagination)
    {
        var futureAdvertisements = databaseContext.Set<AdvertisementDomain>()
            .AsNoTracking()
            .OrderByDescending(advertisement => advertisement.CreatedAt)
            .PaginateWithOffset(pagination)
            .Future();

        var futureCount = databaseContext.Set<AdvertisementDomain>().DeferredCount().FutureValue();
        var advertisements = await futureAdvertisements.ToListAsync();
        var totalCount = await futureCount.ValueAsync();

        return PagedResponse<AdvertisementDomain>.Create(advertisements, pagination, totalCount);
    }
}