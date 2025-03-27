using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Seller;

namespace DealUp.Domain.Advertisement;

public class Advertisement : AuditableEntityBase
{
    private readonly List<AdvertisementMedia> _mediaFiles = [];
    private readonly List<Label> _labels = [];
    private readonly List<Tag> _tags = [];

    public AdvertisementStatus Status { get; private set; }
    public SellerProfile Seller { get; private init; } = null!;
    public Product Product { get; private init; } = null!;
    public Location Location { get; private init; } = null!;
    public AttendanceStatistics Statistics { get; private init; } = null!;

    public IReadOnlyCollection<AdvertisementMedia> MediaFiles
    {
        get => _mediaFiles.AsReadOnly();
        private init => _mediaFiles = value.ToList();
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

    public Advertisement(DateTime createdAt, Product product, Location location, IReadOnlyCollection<AdvertisementMedia> mediaFiles, IReadOnlyCollection<Tag> tags)
    {
        CreatedAt = createdAt;
        Product = product;
        Location = location;
        MediaFiles = mediaFiles;
        Tags = tags;
    }

    public AdvertisementMedia? GetFirstMedia()
    {
        return MediaFiles.FirstOrDefault();
    }

    public decimal GetPrice()
    {
        var priceLabel = _labels.Single(label => label.Name == "price"); // TODO: store default label names?
        return priceLabel.GetLabelValue<decimal>();
    }

    public List<string> ExtractTagValues()
    {
        return _tags.Select(tag => tag.Value).ToList();
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

    public static Advertisement CreateNew(SellerProfile seller, Product product, Location location, List<Label> labels)
    {
        return new Advertisement(AdvertisementStatus.Active)
        {
            Seller = seller,
            Product = product,
            Location = location,
            Labels = labels,
            Statistics = AttendanceStatistics.CreateNew()
        };
    }
}