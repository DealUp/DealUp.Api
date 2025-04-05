using DealUp.DataLake.Interfaces;
using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Interfaces;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Common;
using DealUp.Domain.Media;
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

    public async Task<AdvertisementDomain> CreateAdvertisementAsync(Guid userId, CreateAdvertisementRequest creationRequest)
    {
        var sellerProfile = await sellerRepository.GetSellerProfileAsync(userId);
        if (sellerProfile is null)
        {
            throw new MustCreateSellerProfileException(userId);
        }

        var advertisementMedia = await CreateAdvertisementMediaAsync(creationRequest.SessionId);
        var advertisement = AdvertisementDomain.CreateNew(
            sellerProfile,
            creationRequest.Product,
            creationRequest.Location,
            advertisementMedia,
            creationRequest.Labels,
            creationRequest.Tags);

        var priceLabel = Label.Create("price", creationRequest.Price);
        advertisement.AddAdditionalLabels(priceLabel);
        return await advertisementRepository.CreateAdvertisementAsync(advertisement);
    }

    private async Task<List<MediaEntity>> CreateAdvertisementMediaAsync(Guid sessionId)
    {
        var mediaKeys = await dataLake.GetKeysByPrefixAsync(sessionId.ToString());
        // TODO: implement additional media types (ex. video)
        return mediaKeys.Select(key => MediaEntity.CreateFromKey(key, MediaType.Picture)).ToList();
    }
}