using System;
using System.Collections.Generic;
using System.Linq;
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

        public WeatherDataPrecipitationProbability PrecipitationProbability { get; set; }

        public WeatherText Weather { get; set; }
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