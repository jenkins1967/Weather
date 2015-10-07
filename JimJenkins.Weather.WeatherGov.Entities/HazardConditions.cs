using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class HazardConditions
    {
        [XmlElement("hazard")]
        public Hazard[] Hazard { get; set; }
    }
}