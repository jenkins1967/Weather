using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{
    public class WeatherValue<TValue>
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("units")]
        public string Units { get; set; }
        [XmlAttribute("time-layout")]
        public string TimeLayoutKey { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("value")]
        public TValue[] Values { get; set; }
    }



    //wx
    public class Weather
    {
        [XmlAttribute("time-layout")]
        public string TimeLayoutKey { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("weather-conditions")]
        public WeatherCondition[] WeatherConditions { get; set; }

    }

    public class WeatherCondition
    {
        [XmlElement("value")]
        public WeatherConditionValue[] Values { get; set; }
    }

    public class WeatherConditionValue
    {
        [XmlAttribute("coverage")]
        public string Coverage { get; set; }
        [XmlAttribute("intensity")]
        public string Intensity { get; set; }
        [XmlAttribute("additive")]
        public string Additive { get; set; }
        [XmlAttribute("weather-type")]
        public string WeatherType { get; set; }
        [XmlAttribute("qualifier")]
        public string Qualifier { get; set; }
    }

    //icon
    public class ConditionsIcons
    {
        [XmlAttribute("type")]
        public string Type { get; set; }

        [XmlAttribute("time-layout")]
        public string TimeLayoutKey { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("icon-link")]
        public string[] Link { get; set; }
    }

    //waveh
    public class WaterState
    {
        [XmlAttribute("time-layout")]
        public string TimeLayoutKey { get; set; }

        //waveh
        [XmlElement("waves")]
        public WeatherValue<float?> Waves { get; set; }
    }

    public class ConvectiveHazard
    {
        [XmlElement("severe-component")]
        public WeatherValue<float> SevereComponent { get; set; }
    }
}