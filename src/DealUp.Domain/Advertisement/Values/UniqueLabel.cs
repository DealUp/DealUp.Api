using System.Text.Json;

namespace DealUp.Domain.Advertisement.Values;

public record UniqueLabel
{
    public string Name { get; private set; }
    public string ValueType { get; private set; }
    public JsonDocument Value { get; private set; }

    private UniqueLabel(string name, string valueType, JsonDocument value)
    {
        Name = name;
        ValueType = valueType;
        Value = value;
    }
}