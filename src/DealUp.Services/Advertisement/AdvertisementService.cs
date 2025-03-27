using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Common;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;

namespace DealUp.Services.Advertisement;

public class AdvertisementService(IAdvertisementRepository advertisementRepository) : IAdvertisementService
{
    public Task<PagedResponse<AdvertisementDomain>> GetAllAdvertisementsAsync(PaginationParameters pagination)
    {
        return advertisementRepository.GetAllAdvertisementsAsync(pagination);
    }

    public Task<Guid> CreateAdvertisementAsync(CreateAdvertisementRequest creationRequest)
    {
        throw new NotImplementedException();
    }
}