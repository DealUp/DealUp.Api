using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Advertisement;

public class CreateAdvertisementRequest
{
    public Guid SessionId { get; private set; }
    public decimal Price { get; private set; }
    public Product Product { get; private set; }
    public Location Location { get; private set; }
    public List<Label> Labels { get; private set; }
    public List<Tag> Tags { get; private set; }

    private CreateAdvertisementRequest(Guid sessionId, decimal price, Product product, Location location, List<Label> labels, List<Tag> tags)
    {
        SessionId = sessionId;
        Price = price;
        Product = product;
        Location = location;
        Labels = labels;
        Tags = tags;
    }

    public static CreateAdvertisementRequest Create(Guid sessionId, decimal price, Product product, Location location, List<Label> labels, List<Tag> tags)
    {
        return new CreateAdvertisementRequest(sessionId, price, product, location, labels, tags);
    }
}