using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Head
    {
        [XmlElement("product")]
        public Product Product { get; set; }

        [XmlElement("source")]
        public Source Source { get; set; }
    }
}