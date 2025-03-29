using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class AdvertisementPhoto : EntityBase
{
    public string Url { get; private set; }

    private AdvertisementPhoto(string url)
    {
        Url = url;
    }

    public static AdvertisementPhoto CreateFromUrl(string url)
    {
        return new AdvertisementPhoto(url);
    }
}