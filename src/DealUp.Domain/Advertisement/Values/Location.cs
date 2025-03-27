using NetTopologySuite.Geometries;

namespace DealUp.Domain.Advertisement.Values;

public record Location
{
    public Point Coordinates { get; private set; }

    private Location(Point coordinates)
    {
        Coordinates = coordinates;
    }

    public static Location Create(double longitude, double latitude)
    {
        var point = new Point(longitude, latitude) { SRID = 4326 };
        return new Location(point);
    }
}
