using DealUp.Domain.Common;

namespace DealUp.Domain.Advertisement.Interfaces;

public interface IAdvertisementRepository
{
    public Task<Advertisement> CreateAdvertisementAsync(Advertisement advertisement);
    public Task<PagedResponse<Advertisement>> GetAllAdvertisementsAsync(PaginationParameters pagination);
    public Task<List<Label>> GetExistingLabelsAsync(List<Label> labelsToCheck);
    public Task<List<Label>> CreateLabelsAsync(List<Label> labels);
}