using DealUp.Domain.Advertisement;
using DealUp.Domain.Advertisement.Values;
using DealUp.Dto.v1.Advertisement;
using AdvertisementDomain = DealUp.Domain.Advertisement.Advertisement;

namespace DealUp.Application.Api.Controllers.v1.Advertisement;

public static class Converter
{
    public static List<AdvertisementSummaryDto> ToEnumerableDto(this IEnumerable<AdvertisementDomain> advertisements)
    {
        return advertisements.Select(ToDto).ToList();
    }

    private static AdvertisementSummaryDto ToDto(this AdvertisementDomain advertisement)
    {
        return new AdvertisementSummaryDto
        {
            Id = advertisement.Id,
            ProductName = advertisement.Product.Title,
            Price = advertisement.GetPrice(),
            MainPhotoUrl = advertisement.GetFirstMedia()?.Url,
            CreationDate = advertisement.CreatedAt,
            Tags = advertisement.ExtractTagValues()
        };
    }

    public static CreateAdvertisementRequest ToDomain(this CreateAdvertisementDto createAdvertisement)
    {
        var product = Product.Create(createAdvertisement.Title, createAdvertisement.Description);
        var location = Location.Create(createAdvertisement.Location.Longitude, createAdvertisement.Location.Latitude);
        var labels = createAdvertisement.Labels.Select(label => Label.Create(label.Name, label.Value)).ToList();
        var tags = createAdvertisement.Tags.Select(Tag.Create).ToList();

        return CreateAdvertisementRequest.Create(product, location, labels, tags);
    }
}