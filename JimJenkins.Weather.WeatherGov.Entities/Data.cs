using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Data
    {
        [XmlElement("location")]
        public Location[] Locations { get; set; }

        [XmlElement("moreWeatherInformation")]
        public MoreInformation[] MoreInformations { get; set; }

        [XmlElement("time-layout")]
        public TimeLayout[] TimeLayouts { get; set; }

        [XmlElement("parameters")]
        public Parameters[] Parameters { get; set; }
        
    }
}