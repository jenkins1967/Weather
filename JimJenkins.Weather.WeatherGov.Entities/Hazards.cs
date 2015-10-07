using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Hazards
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlAttribute("time-layout")]
        public string TimeLayoutKey { get; set; }

        [XmlElement("hazard-conditions")]
        public HazardConditions HazardConditions { get; set; }
    }
}