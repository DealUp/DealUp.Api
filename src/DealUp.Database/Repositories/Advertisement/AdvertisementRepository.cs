using DealUp.Database.Extensions;
using DealUp.Database.Interfaces;
using DealUp.Domain.Advertisement;
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

    public async Task<List<Label>> GetExistingLabelsAsync(List<Label> labelsToCheck)
    {
        return await databaseContext.Set<Label>()
            .Where(existingLabel => labelsToCheck.Select(label => label.Name).Contains(existingLabel.Name))
            .Where(existingLabel => labelsToCheck.Select(label => label.Value).Contains(existingLabel.Value))
            .ToListAsync();
    }

    public async Task<List<Tag>> GetExistingTagsAsync(List<Tag> tagsToCheck)
    {
        return await databaseContext.Set<Tag>()
            .Where(existingTag => tagsToCheck.Select(tag => tag.Value).Contains(existingTag.Value))
            .ToListAsync();
    }
}