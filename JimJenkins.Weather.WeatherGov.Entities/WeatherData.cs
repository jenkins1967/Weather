using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    [XmlRoot("dwml")]
    public class WeatherData
    {
        [XmlElement("head")]
        public Head Head { get; set; }

        [XmlElement("data")]
        public Data Data { get; set; }
    }


    



}
