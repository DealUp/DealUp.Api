using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class Tag : EntityBase
{
    private readonly List<Advertisement> _advertisements = [];

    public string Value { get; private set; }
    public IReadOnlyCollection<Advertisement> Advertisements => _advertisements.AsReadOnly();

    private Tag(string value)
    {
        Value = value;
    }

    public static Tag Create(string value)
    {
        return new Tag(value);
    }
}