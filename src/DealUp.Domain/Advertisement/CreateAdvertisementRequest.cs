using DealUp.Domain.Advertisement.Values;

namespace DealUp.Domain.Advertisement;

public class CreateAdvertisementRequest
{
    public Product Product { get; private set; }
    public Location Location { get; private set; }
    public List<Label> Labels { get; private set; }
    public List<Tag> Tags { get; private set; }

    private CreateAdvertisementRequest(Product product, Location location, List<Label> labels, List<Tag> tags)
    {
        Product = product;
        Location = location;
        Labels = labels;
        Tags = tags;
    }

    public List<Label> GetLabelsToCreate(List<Label> existingLabels)
    {
        var existingLabelValues = existingLabels.Select(label => new { label.Name, label.Value });
        return Labels.ExceptBy(existingLabelValues, label => new { label.Name, label.Value }).ToList();
    }

    public List<Tag> GetTagsToCreate(List<Tag> existingTags)
    {
        var existingTagValues = existingTags.Select(tag => tag.Value);
        return Tags.ExceptBy(existingTagValues, tag => tag.Value).ToList();
    }

    public static CreateAdvertisementRequest Create(Product product, Location location, List<Label> labels, List<Tag> tags)
    {
        return new CreateAdvertisementRequest(product, location, labels, tags);
    }
}