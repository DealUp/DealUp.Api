using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Seller;

namespace DealUp.Domain.Advertisement;

public class Advertisement : EntityBase
{
    private readonly List<AdvertisementPhoto> _photos = [];
    private readonly List<Label> _labels = [];
    private readonly List<Tag> _tags = [];

    public AdvertisementStatus Status { get; private set; }
    public SellerProfile Seller { get; private init; } = null!;
    public Product Product { get; private init; } = null!;
    public Location Location { get; private init; } = null!;
    public AttendanceStatistics Statistics { get; private init; } = null!;

    public IReadOnlyCollection<AdvertisementPhoto> Photos
    {
        get => _photos.AsReadOnly();
        private init => _photos = value.ToList();
    }

    public IReadOnlyCollection<Label> Labels
    {
        get => _labels.AsReadOnly();
        private init => _labels = value.ToList();
    }

    public IReadOnlyCollection<Tag> Tags
    {
        get => _tags.AsReadOnly();
        private init => _tags = value.ToList();
    }

    private Advertisement(AdvertisementStatus status)
    {
        Status = status;
    }

    public void AddTag(Tag tag)
    {
        if (!_tags.Contains(tag))
        {
            _tags.Add(tag);
        }
    }

    public void RemoveTag(string tagName)
    {
        var tag = _tags.FirstOrDefault(t => t.Value == tagName);
        if (tag is not null)
        {
            _tags.Remove(tag);
        }
    }

    public static Advertisement CreateNew(
        SellerProfile seller,
        Product product,
        Location location,
        List<AdvertisementPhoto> photos,
        List<Tag> tags)
    {
        return new Advertisement(AdvertisementStatus.Active)
        {
            Seller = seller,
            Product = product,
            Location = location,
            Photos = photos,
            Tags = tags,
            Statistics = AttendanceStatistics.CreateNew()
        };
    }
}