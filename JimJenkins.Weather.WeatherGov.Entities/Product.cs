using System;
using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Product {
        [XmlElement("title")]
        public string Title { get; set; }

        [XmlElement("field")]
        public string Field { get; set; }
        [XmlElement("category")]
        public string Category { get; set; }

        [XmlElement("creation-date")]
        public DateTime CreationDate { get; set; }
    }
}