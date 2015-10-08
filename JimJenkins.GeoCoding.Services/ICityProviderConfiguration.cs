using System;

namespace JimJenkins.GeoCoding.Services
{
    public interface ICityListProviderConfiguration
    {
        Uri BaseUri { get; }
    }

    public class CityListProviderConfiguration : ICityListProviderConfiguration
    {
        public CityListProviderConfiguration()
        {
            //todo move to config
            BaseUri = new Uri("http://graphical.weather.gov/xml/sample_products/browser_interface/ndfdXMLclient.php");
        }
        public Uri BaseUri { get; private set; }
    }
}