namespace DealUp.Dto.v1.Advertisement;

public record LocationDto
{
    public required double Longitude { get; set; }
    public required double Latitude { get; set; }
}