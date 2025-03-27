using DealUp.Domain.Common;

namespace DealUp.Domain.Advertisement.Interfaces;

public interface IAdvertisementRepository
{
    public Task<PagedResponse<Advertisement>> GetAllAdvertisementsAsync(PaginationParameters pagination);
}