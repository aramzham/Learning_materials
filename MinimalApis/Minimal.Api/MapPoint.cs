namespace Minimal.Api;

public class MapPoint
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    public static bool TryParse(string? stringValue, out MapPoint? point)
    {
        try
        {
            var splitByComma = stringValue.Split(',').Select(double.Parse).ToArray();
            point = new MapPoint() { Latitude = splitByComma[0], Longitude = splitByComma[1]};
            return true;
        }
        catch
        {
            point = null;
            return false;
        }
    }
}