using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimJenkins.GeoCoding.Services
{
    public class GeoCodingResult
    {
        public Coordinate Coordinate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Coordinate
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
    }

    public class City
    {
        public Coordinate GeoCoordinates { get; set; }
        public string Name { get; set; }
    }
}
