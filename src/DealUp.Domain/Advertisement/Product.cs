using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class Product : AuditableEntityBase
{
    private readonly List<Label> _labels = [];

    public string Title { get; private set; }
    public string Description { get; private set; }

    public IReadOnlyCollection<Label> Labels
    {
        get => _labels.AsReadOnly();
        private init => _labels = value.ToList();
    }

    private Product(string title, string description)
    {
        Title = title;
        Description = description;
    }

    public static Product Create(string title, string description)
    {
        return new Product(title, description);
    }
}