namespace DealUp.Dto.v1.Advertisement;

public record AdvertisementSummaryDto
{
    public required Guid Id { get; set; }
    public required string ProductName { get; set; }
    public required decimal Price { get; set; }
    public required string? MainPhotoUrl { get; set; }
    public required DateTime CreationDate { get; set; }
    public required List<string> Tags { get; set; }
    // TODO: return location after additional discussion
}