using System.IO;
using System.Linq;
using JimJenkins.GeoCoding.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.GeoCodingCityProvider
{
    [TestClass]
    public class CityParserSpecs
    {
        private readonly string _data;
        private ICityListParser _sut;
        public CityParserSpecs()
        {
            _data = File.ReadAllText(@"SampleXml\SampleCityList.xml");
        }

        [TestInitialize]
        public void Initialize()
        {
            _sut = new CityListParser();
        }

        [TestMethod]
        public void should_load_all_cities()
        {
            var list = _sut.Parse(_data);
            list.Count().ShouldEqual(495);
        }

        [TestMethod]
        public void should_load_both_city_and_coordinates()
        {
            var list = _sut.Parse(_data);
            foreach (var city in list)
            {
                city.Name.ShouldNotBeEmpty();
                city.GeoCoordinates.Latitude.ShouldNotEqual(0);
                city.GeoCoordinates.Longitude.ShouldNotEqual(0);
            }
        }
    }
}