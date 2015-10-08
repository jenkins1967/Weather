using System;
using System.Collections.Generic;
using System.Net;

namespace JimJenkins.GeoCoding.Services
{
    public interface ICityListProvider
    {
        IEnumerable<City> GetAllCities();
        IEnumerable<City> GetPrimaryCities();
    }

    public class CityListProvider : ICityListProvider
    {
        private readonly ICityListProviderConfiguration _config;
        private readonly ICityListParser _cityParser;

        public CityListProvider(ICityListProviderConfiguration config, ICityListParser cityParser)
        {
            _config = config;
            _cityParser = cityParser;
        }

        public IEnumerable<City> GetAllCities()
        {
            return GetCities("1234");
        }

        public IEnumerable<City> GetPrimaryCities()
        {
            return GetCities("1");
        }



        private IEnumerable<City> GetCities(string flags)
        {        
            var requestUri = new UriBuilder(_config.BaseUri){Query = "listCitiesLevel=" + flags};
            var client = new WebClient();
            var data = client.DownloadString(requestUri.Uri);
            return _cityParser.Parse(data);
        }
    }
}