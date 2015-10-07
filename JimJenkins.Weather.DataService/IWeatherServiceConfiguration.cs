using System;

namespace JimJenkins.Weather.WeatherGov.DataService
{

    public interface IWeatherServiceConfiguration
    {
        Uri BaseUri { get; }
    }

    public class WeatherServiceConfiguration : IWeatherServiceConfiguration
    {
        public WeatherServiceConfiguration()
        {
            //todo move to config
            BaseUri = new Uri("http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php");
        }
        public Uri BaseUri { get; private set; }
    }
}
