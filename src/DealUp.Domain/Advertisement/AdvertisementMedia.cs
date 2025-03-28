using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Advertisement;

public class AdvertisementMedia : EntityBase
{
    public string Key { get; private set; }
    public MediaType Type { get; private set; }

    private AdvertisementMedia(string key, MediaType type)
    {
        Key = key;
        Type = type;
    }

    public static AdvertisementMedia CreateFromKey(string key, MediaType type)
    {
        return new AdvertisementMedia(key, type);
    }
}