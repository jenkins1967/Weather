using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class MoreInformation
    {
        [XmlAttribute("applicable-location")]
        public string LocationKey { get; set; }
    }
}