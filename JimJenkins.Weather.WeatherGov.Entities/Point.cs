using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Point
    {
        [XmlAttribute("latitude")]
        public float Latitude { get; set; }

        [XmlAttribute("longitude")]
        public float Longitude { get; set; }
    }
}