using DealUp.Domain.Abstractions;
using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Advertisement;

public class Product : EntityBase
{
    public string Title { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }

    private readonly List<Label> _labels = [];
    public IReadOnlyList<Label> Labels
    {
        get => _labels.AsReadOnly();
        private init => _labels = value.ToList();
    }

    private Product(string title, string description, decimal price)
    {
        Title = title;
        Description = description;
        Price = price;
    }

    public void AddLabel(Label label)
    {
        var existingLabel = _labels.FirstOrDefault(l => l.Name == label.Name);
        if (existingLabel is not null)
        {
            _labels.Remove(existingLabel);
        }
        _labels.Add(label);
    }

    public void RemoveLabel(string labelName)
    {
        var label = _labels.FirstOrDefault(l => l.Name == labelName);
        if (label is not null)
        {
            _labels.Remove(label);
        }
    }

    public static Product Create(string title, string description, decimal price)
    {
        return new Product(title, description, price);
    }
}