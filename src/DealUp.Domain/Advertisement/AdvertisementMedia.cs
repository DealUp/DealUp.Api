using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Advertisement;

public class AdvertisementMedia : EntityBase
{
    public string Url { get; private set; }
    public MediaType Type { get; private set; }

    private AdvertisementMedia(string url, MediaType type)
    {
        Url = url;
        Type = type;
    }

    public static AdvertisementMedia CreateFromUrl(string url, MediaType type)
    {
        return new AdvertisementMedia(url, type);
    }
}