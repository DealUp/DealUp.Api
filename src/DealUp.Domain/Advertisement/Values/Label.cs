namespace DealUp.Domain.Advertisement.Values;

public record Label
{
    public string Name { get; private set; }
    public string Value { get; private set; }

    private Label(string name, string value)
    {
        Name = name;
        Value = value;
    }

    public static Label Create(string name, string value)
    {
        return new Label(name, value);
    }
}