namespace DealUp.Dto.v1.Advertisement;

public record CreateAdvertisementDto
{
    public Guid SessionId { get; private set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required decimal Price { get; set; }
    public required LocationDto Location { get; set; }
    public List<string> Tags { get; set; } = [];
    public List<LabelDto> Labels { get; set; } = [];
}