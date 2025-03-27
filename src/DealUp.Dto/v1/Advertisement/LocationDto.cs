namespace DealUp.Dto.v1.Advertisement;

public record LocationDto
{
    public required decimal Latitude { get; set; }
    public required decimal Longitude { get; set; }
}