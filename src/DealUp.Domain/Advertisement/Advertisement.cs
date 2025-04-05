using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Media;
using DealUp.Domain.Seller;

namespace DealUp.Domain.Advertisement;

public class Advertisement : AuditableEntityBase
{
    private readonly List<MediaEntity> _media = [];
    private readonly List<Label> _labels = [];
    private readonly List<Tag> _tags = [];

    public AdvertisementStatus Status { get; private set; }
    public SellerProfile Seller { get; private init; } = null!;
    public Product Product { get; private init; } = null!;
    public Location Location { get; private init; } = null!;
    public AttendanceStatistics Statistics { get; private init; } = null!;

    public IReadOnlyCollection<MediaEntity> Media
    {
        get => _media.AsReadOnly();
        private init => _media = value.ToList();
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

    public MediaEntity? GetFirstMedia()
    {
        return Media.FirstOrDefault();
    }

    public decimal GetPrice()
    {
        var priceLabel = _labels.Single(label => label.Name == "price"); // TODO: store default label names?
        return priceLabel.GetValue<decimal>();
    }

    public List<string> ExtractTagValues()
    {
        return _tags.Select(tag => tag.Value).ToList();
    }

    public void AddAdditionalLabels(params List<Label> labels)
    {
        foreach (var label in labels)
        {
            AddOrUpdateLabel(label);
        }
    }

    public void AddOrUpdateLabel(Label label)
    {
        var existingLabel = _labels.FirstOrDefault(existingLabel => existingLabel.Name == label.Name);

        if (existingLabel is null)
        {
            _labels.Add(label);
        }
        else
        {
            existingLabel.SetValue(label);
        }
    }

    public static Advertisement CreateNew(
        SellerProfile seller,
        Product product,
        Location location,
        List<MediaEntity> mediaFiles,
        List<Label> labels,
        List<Tag> tags)
    {
        return new Advertisement(AdvertisementStatus.Active)
        {
            Seller = seller,
            Product = product,
            Location = location,
            Media = mediaFiles,
            Labels = labels,
            Tags = tags,
            Statistics = AttendanceStatistics.CreateNew()
        };
    }
}