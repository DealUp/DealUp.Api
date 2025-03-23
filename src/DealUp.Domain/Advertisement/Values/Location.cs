namespace DealUp.Domain.Advertisement.Values;

public record Location
{
    public decimal Longitude { get; private set; }
    public decimal Latitude { get; private set; }

    private Location(decimal longitude, decimal latitude)
    {
        Longitude = longitude;
        Latitude = latitude;
    }

    public static Location Create(decimal longitude, decimal latitude)
    {
        return new Location(longitude, latitude);
    }
}
