using DealUp.Domain.Abstractions;

namespace DealUp.Domain.Advertisement;

public class Tag : EntityBase
{
    public string Value { get; private set; }
    public Advertisement Advertisement { get; private init; } = null!;

    private Tag(string value)
    {
        Value = value;
    }

    public static Tag Create(string value)
    {
        return new Tag(value);
    }
}