using System.Web.Http;
using JimJenkins.Weather.WeatherGov.DataService;
using JimJenkins.Weather.WeatherGov.Entities;
using Web.Models;

namespace Web.Controllers
{
    public class WeatherController : ApiController
    {
        private readonly IWeatherService _weatherService;
        private readonly IWeatherResultFactory _weatherResultFactory;

        public WeatherController(IWeatherService weatherService, IWeatherResultFactory weatherResultFactory)
        {
            _weatherService = weatherService;
            _weatherResultFactory = weatherResultFactory;
        }

        [HttpGet]
        public WeatherData WeatherForLocation([FromUri] CoordinateViewModel viewModel)
        {
            var location = new CoordinateRequestLocationProvider(viewModel.Latitude, viewModel.Longitude);            
            var request = new WeatherRequest(location,  RequestElementsProvider.AllElements);
            var data = _weatherService.GetData(request);
            var result = _weatherResultFactory.Build(data);
            
            return data;
            // return 

        }


    }
}