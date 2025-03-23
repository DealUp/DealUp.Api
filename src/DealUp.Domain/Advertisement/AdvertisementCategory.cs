using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class AdvertisementCategory : EntityBase
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private AdvertisementCategory(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public static AdvertisementCategory Create(string name, string description)
    {
        return new AdvertisementCategory(name, description);
    }
}