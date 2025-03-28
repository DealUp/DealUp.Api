using DealUp.DataLake.Interfaces;
using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Common;
using DealUp.Domain.Seller.Interfaces;
using DealUp.Exceptions;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;

namespace DealUp.Services.Advertisement;

public class AdvertisementService(IDataLake dataLake, ISellerRepository sellerRepository, IAdvertisementRepository advertisementRepository) : IAdvertisementService
{
    public Task<PagedResponse<AdvertisementDomain>> GetAllAdvertisementsAsync(PaginationParameters pagination)
    {
        return advertisementRepository.GetAllAdvertisementsAsync(pagination);
    }

    public async Task<AdvertisementDomain> CreateAdvertisementAsync(Guid userId, Guid sessionId, CreateAdvertisementRequest creationRequest)
    {
        var sellerProfile = await sellerRepository.GetSellerProfileAsync(userId);
        if (sellerProfile is null)
        {
            throw new MustCreateSellerProfileException(userId);
        }

        var advertisementLabels = await CreateAdvertisementLabelsAsync(creationRequest);
        var advertisementMedia = await CreateAdvertisementMediaAsync(sessionId);
        var advertisementTags = await CreateAdvertisementTagsAsync(creationRequest);

        var advertisement = AdvertisementDomain.CreateNew(
            sellerProfile,
            creationRequest.Product,
            creationRequest.Location,
            advertisementMedia,
            advertisementLabels,
            advertisementTags);

        return await advertisementRepository.CreateAdvertisementAsync(advertisement);
    }

    private async Task<List<Label>> CreateAdvertisementLabelsAsync(CreateAdvertisementRequest creationRequest)
    {
        var existingLabels = await advertisementRepository.GetExistingLabelsAsync(creationRequest.Labels);
        var labelsToCreate = creationRequest.GetLabelsToCreate(existingLabels);
        return [..existingLabels, ..labelsToCreate];
    }

    private async Task<List<AdvertisementMedia>> CreateAdvertisementMediaAsync(Guid sessionId)
    {
        var mediaKeys = await dataLake.GetKeysByPrefixAsync(sessionId.ToString());
        // TODO: discuss & implement additional media types (ex. video)
        return mediaKeys.Select(key => AdvertisementMedia.CreateFromKey(key, MediaType.Picture)).ToList();
    }

    private async Task<List<Tag>> CreateAdvertisementTagsAsync(CreateAdvertisementRequest creationRequest)
    {
        var existingTags = await advertisementRepository.GetExistingTagsAsync(creationRequest.Tags);
        var tagsToCreate = creationRequest.GetTagsToCreate(existingTags);
        return [..existingTags, ..tagsToCreate];
    }
}