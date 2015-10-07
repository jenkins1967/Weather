using System;
using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class TimeLayout
    {
        [XmlAttribute("time-coordinate")]
        public string TimeCoordinate { get; set; }

        [XmlAttribute("summarization")]
        public string Summarization { get; set; }

        [XmlElement("layout-key")]
        public string Key { get; set; }

        [XmlElement("start-valid-time")]
        public DateTime[] StartTimes{get;set;}

        [XmlElement("end-valid-time")]
        public DateTime[] EndTimes { get; set; }
    }
}