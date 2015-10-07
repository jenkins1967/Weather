using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Location
    {
        [XmlElement("location-key")]
        public string Key { get; set; }

        [XmlElement("point")]
        public Point Point { get; set; }
    }
}