using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Media;

public class MediaEntity : AuditableEntityBase
{
    public string Key { get; private set; }
    public MediaType Type { get; private set; }

    private MediaEntity(string key, MediaType type)
    {
        Key = key;
        Type = type;
    }

    public static MediaEntity CreateFromKey(string key, MediaType type)
    {
        return new MediaEntity(key, type);
    }
}