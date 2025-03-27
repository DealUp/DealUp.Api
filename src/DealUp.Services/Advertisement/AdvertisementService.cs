using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Common;
using DealUp.Domain.Seller.Interfaces;
using DealUp.Exceptions;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;

namespace DealUp.Services.Advertisement;

public class AdvertisementService(ISellerRepository sellerRepository, IAdvertisementRepository advertisementRepository) : IAdvertisementService
{
    public Task<PagedResponse<AdvertisementDomain>> GetAllAdvertisementsAsync(PaginationParameters pagination)
    {
        return advertisementRepository.GetAllAdvertisementsAsync(pagination);
    }

    public async Task<AdvertisementDomain> CreateAdvertisementAsync(Guid userId, CreateAdvertisementRequest creationRequest)
    {
        var sellerProfile = await sellerRepository.GetSellerProfileAsync(userId);
        if (sellerProfile is null)
        {
            throw new MustCreateSellerProfileException(userId);
        }

        List<Label> advertisementLabels = await CreateAdvertisementLabelsAsync(creationRequest);
        var advertisement = AdvertisementDomain.CreateNew(sellerProfile, creationRequest.Product, creationRequest.Location, advertisementLabels);
        return await advertisementRepository.CreateAdvertisementAsync(advertisement);
    }

    private async Task<List<Label>> CreateAdvertisementLabelsAsync(CreateAdvertisementRequest creationRequest)
    {
        var existingLabels = await advertisementRepository.GetExistingLabelsAsync(creationRequest.Labels);
        var labelsToCreate = creationRequest.GetLabelsToCreate(existingLabels);
        var newlyCreatedLabels = await advertisementRepository.CreateLabelsAsync(labelsToCreate);

        return [..existingLabels, ..newlyCreatedLabels];
    }
}