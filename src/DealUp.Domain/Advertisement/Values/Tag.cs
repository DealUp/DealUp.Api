namespace DealUp.Domain.Advertisement.Values;

public record Tag
{
    public string Name { get; private set; }

    private Tag(string name)
    {
        Name = name;
    }

    public static Tag Create(string name)
    {
        return new Tag(name);
    }
}