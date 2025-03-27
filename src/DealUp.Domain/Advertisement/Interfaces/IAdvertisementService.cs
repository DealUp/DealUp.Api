using DealUp.Domain.Common;

namespace DealUp.Domain.Advertisement.Interfaces;

public interface IAdvertisementService
{
    public Task<PagedResponse<Advertisement>> GetAllAdvertisementsAsync(PaginationParameters pagination);
    public Task<Guid> CreateAdvertisementAsync(CreateAdvertisementRequest creationRequest);
}