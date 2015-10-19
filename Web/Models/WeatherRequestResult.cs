using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Newtonsoft.Json;

namespace Web.Models
{
    public class WeatherRequestResult
    {
        public string CreditUrl { get; set; }
        public string CreditLogUrl { get; set; }

        public DateTime CreatedOnUtc { get; set; }
        public WeatherDataTemperature Temperatures { get; set; }
        public WeatherDataPrecipitation Precipitation { get; set; }

        public WeatherDataWind Wind { get; set; }
        public WeatherDataPrecipitationProbability PrecipitationProbability { get; set; }

        public WeatherDataCloudCover CloudCover { get; set; }


        //public WeatherText Weather { get; set; }
    }

    public class WeatherText
    {
        public IEnumerable<WeatherData<string>> WeatherType { get; set; }
    }

    public class WeatherDataPrecipitationProbability
    {
        public IEnumerable<WeatherData<Int32>> Percents { get; set; }
    }

    public class WeatherDataTemperature
    {
        public IEnumerable<WeatherData<Int32>> DailyMaximum { get; set; }
        public IEnumerable<WeatherData<Int32>> DailyMinimum{ get; set; }
        public IEnumerable<WeatherData<Int32>> Hourly { get; set; }

        public IEnumerable<WeatherData<Int32>> DewPoint { get; set; }

        public IEnumerable<WeatherData<Int32>> Apparent { get; set; }
    }

    public class WeatherDataCloudCover
    {
        [DataMember(Name="Percent")]
        public IEnumerable<WeatherData<Int32?>> Percent { get; set; }
    }

    public class WeatherDataWind
    {
        public IEnumerable<WeatherData<Int32?>> Speed { get; set; }
        public IEnumerable<WeatherData<Int32>> Direction { get; set; }
        public IEnumerable<WeatherData<Int32?>> Gusts { get; set; }
    }

    public class WeatherDataPrecipitation
    {
        public IEnumerable<WeatherData<Int32>> Liquid { get; set; }
        public IEnumerable<WeatherData<Int32>> Ice { get; set; }
        public IEnumerable<WeatherData<Int32>> Snow { get; set; }
    }

    public class WeatherData<TType>
    {
        public TType Value { get; set; }
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }
    }
}