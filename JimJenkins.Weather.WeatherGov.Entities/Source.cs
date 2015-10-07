using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class Source { 
        [XmlElement("more-information")]
        public string MoreInformation { get; set; }
        //[XmlElement("production-center")]
        //public string ProductionCenter{get;set;}
        [XmlElement("disclaimer")]
        public string Disclaimer{get;set;}

        [XmlElement("credit")]
        public string Credit{get;set;}

        [XmlElement("credit-logo")]
        public string CreditLogo{get;set;}

        [XmlElement("feedback")]
        public string Feedback{get;set;}

    }
}