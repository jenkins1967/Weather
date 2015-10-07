using System.Collections.Generic;

namespace JimJenkins.GeoCoding.Services
{
    public interface ICityListProvider
    {
        IEnumerable<City> GetCities();
    }

    public class CityListProvider : ICityListProvider
    {
        private readonly ICityListParser _cityParser;

        public CityListProvider(ICityListParser cityParser)
        {
            _cityParser = cityParser;
        }

        public IEnumerable<City> GetCities()
        {
            var service = new CityService.ndfdXMLPortTypeClient();
            var data = service.LatLonListCityNames("1234");
            return _cityParser.Parse(data);
        }
    }
}