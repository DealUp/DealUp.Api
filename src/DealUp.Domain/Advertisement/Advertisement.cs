using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;
using DealUp.Domain.Seller;

namespace DealUp.Domain.Advertisement;

/*
 * Нахуй категорії
 *
 * Залишаємо теги
 * Залишаємо лейбли в продукті (колір, стан і т.д.)
 * Додаємо лейбли в оголошення (зробимо словник, подумати про типи, додати методи для отримання дефолтних лейблів (ціна, локація і подумати))
 *
 * !!! Теги і лейбли винести нахуй в окремі таблиці !!!
 *
 */
public class Advertisement : EntityBase
{
    public SellerProfile Seller { get; private init; } = null!;
    public Product Product { get; private init; } = null!;
    public Location Location { get; private init; } = null!;
    public AttendanceStatistics Statistics { get; private init; } = null!;
    public AdvertisementStatus Status { get; private set; }

    // public decimal Price { get; private set; } // TODO:

    private readonly List<AdvertisementPhoto> _photos = [];
    public IReadOnlyCollection<AdvertisementPhoto> Photos
    {
        get => _photos.AsReadOnly();
        private init => _photos = value.ToList();
    }

    private readonly List<Tag> _tags = [];
    public IReadOnlyList<Tag> Tags
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
        var tag = _tags.FirstOrDefault(t => t.Name == tagName);
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