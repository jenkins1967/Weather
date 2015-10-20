using System.Globalization;

namespace JimJenkins.GeoCoding.Services
{
    public class Coordinate
    {
        public Coordinate(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
        public float Latitude { get; private set; }
        public float Longitude { get; private set; }

        public override int GetHashCode()
        {
            return (Latitude.ToString(CultureInfo.InvariantCulture) + 
                Longitude.ToString(CultureInfo.InvariantCulture))
                .GetHashCode();
        }
    }
}