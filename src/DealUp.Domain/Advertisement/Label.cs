using System.Text.Json;
using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class Label : EntityBase
{
    public string Name { get; private set; }
    public string ValueType { get; private set; }
    public JsonDocument Value { get; private set; }
    public Advertisement? Advertisement { get; private init; }
    public Product? Product { get; private init; }

    private Label(string name, string valueType, JsonDocument value)
    {
        Name = name;
        ValueType = valueType;
        Value = value;
    }

    public TValue GetValue<TValue>()
    {
        var targetType = Type.GetType(ValueType);

        if (targetType is null || targetType != typeof(TValue))
        {
            throw new InvalidOperationException($"Type {ValueType} is not supported.");
        }

        return Value.Deserialize<TValue>()!;
    }

    public void SetValue(Label otherLabel)
    {
        ValueType = otherLabel.ValueType;
        Value = otherLabel.Value;
    }

    public static Label Create<TValue>(string name, TValue value)
    {
        var valueType = typeof(TValue).FullName!;
        var serializedValue = JsonSerializer.SerializeToDocument(value);
        return new Label(name, valueType, serializedValue);
    }
}