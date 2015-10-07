using System;
using JimJenkins.Weather.WeatherGov.DataService;
using JimJenkins.Weather.WeatherGov.Entities.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.Service
{
    [TestClass]
    public class ServiceConnectivity
    {
        private IWeatherService _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new WeatherService(new WeatherServiceConfiguration(), new WeatherParser());
        }
        [TestMethod]
        public void should_return_something()
        {
            //arrange
            var location = new CoordinateRequestLocationProvider(40.34F, -74.112F);
            var elements = new RequestElementsProvider {MaximumTemperature = true, WindDirection = true};
            var request = new WeatherRequest(location, elements);

            //act
            var result = _sut.GetData(request);
            result.ShouldNotBeNull();
        }
    }
}
