using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Hazard
    {
        [XmlAttribute("hazardCode")]
        public string Code { get; set; }

        [XmlAttribute("phenomena")]
        public string Phenomena { get; set; }

        [XmlAttribute("significance")]
        public string Significance { get; set; }

        [XmlAttribute("hazardType")]
        public string Type { get; set; }

        [XmlElement("hazardTextURL")]
        public string TextUrl { get; set; }
    }
}