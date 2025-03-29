using System.Text.Json;
using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class Label : EntityBase
{
    private readonly List<Advertisement> _advertisements = [];
    private readonly List<Product> _products = [];

    public string Name { get; private set; }
    public string ValueType { get; private set; }
    public JsonDocument Value { get; private set; }

    public IReadOnlyCollection<Advertisement> Advertisements => _advertisements.AsReadOnly();
    public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

    private Label(string name, string valueType, JsonDocument value)
    {
        Name = name;
        ValueType = valueType;
        Value = value;
    }

    public TValue GetLabelValue<TValue>()
    {
        var targetType = Type.GetType(ValueType);

        if (targetType is null || targetType != typeof(TValue))
        {
            throw new InvalidOperationException($"Type {ValueType} is not supported.");
        }

        var typedValue = Value.Deserialize(targetType)!;
        return (TValue)typedValue;
    }

    public static Label Create(string name, object value)
    {
        var valueType = value.GetType().FullName!;
        var serializedValue = JsonSerializer.SerializeToDocument(value);
        return new Label(name, valueType, serializedValue);
    }
}