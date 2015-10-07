using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities
{

    /// <summary>
    /// There is a Parameter object for each Location
    /// </summary>
    public class Parameters
    {
        [XmlAttribute("applicable_location")]
        public string LocationKey { get; set; }
        
        //maxt, mint, temp, dew, appt
        [XmlElement("temperature")]
        public WeatherValue<float>[] Temperatures { get; set; }

        //wspd, cumw34, cumw50, cumw64, wgust
        [XmlElement("wind-speed")]
        public WeatherValue<float?>[] WindSpeeds { get; set; }

        //wdir
        [XmlElement("direction")]
        public WeatherValue<int>[] WindDirections { get; set; }

        //sky
        [XmlElement("cloud-amount")]
        public WeatherValue<float?>[] CloudAmounts { get; set; }

        //qpf (liquid precip), snow
        [XmlElement("precipitation")]
        public WeatherValue<float>[] Precipitations { get; set; }

        //pop12
        [XmlElement("probability-of-precipitation")]
        public WeatherValue<float>[] ProbabilityOfPrecipitations { get; set; }

        //rh
        [XmlElement("humidity")]
        public WeatherValue<float>[] Humidities { get; set; }

        //wwa
        [XmlElement("hazards")]
        public Hazards[] Hazards { get; set; }

        //wx
        [XmlElement("weather")]
        public Weather[] Weathers { get; set; }

        //icons
        [XmlElement("conditions-icon")]
        public ConditionsIcons Icons { get; set; }

        //waveh
        [XmlElement("water-state")]
        public WaterState WaterState { get; set; }

        //ptornado=ptornado&phail=phail&ptotsvrtstm=ptotsvrtstm
        [XmlElement("convective-hazard")]
        public ConvectiveHazard[] ConvectiveHazard { get; set; }
    }
}