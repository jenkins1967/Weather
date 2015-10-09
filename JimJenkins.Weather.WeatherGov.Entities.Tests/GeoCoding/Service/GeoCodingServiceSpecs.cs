using System;
using System.Linq;
using JimJenkins.GeoCoding.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.GeoCoding.Service
{
    [TestClass]
    public class GeoCodingServiceSpecs
    {
        private readonly IGeoCodingService _sut;

        public GeoCodingServiceSpecs()
        {
            var key = ConfigurationManager.AppSettings["geoLocationKey"];
            _sut = new GeoCodingService(new GeoCodingServiceConfigProvider(key), new GeoCodingParser(), new GeoCodingDataMapper());
        }
       
        [TestMethod]
        public void should_find_by_zip_code()
        {
            var result = _sut.GetFromZip("08610");
            result.City.ShouldEqual("Trenton");
            result.State.ShouldEqual("New Jersey");

        }

        [TestMethod]
        public void should_find_by_city_state()
        {
            var result = _sut.GetFromCityState("Trenton", "NJ");
            result.City.ShouldEqual("Trenton");
            result.State.ShouldEqual("New Jersey");

        }

        [TestMethod]
        public void should_find_city_state_without_comma()
        {
            var result = _sut.GetFromAddress("Trenton NJ").FirstOrDefault();
            result.City.ShouldEqual("Trenton");
            result.State.ShouldEqual("New Jersey");

        }

        [TestMethod]
        public void should_find_city_only()
        {
            var result = _sut.GetFromAddress("Levittown");
            result.All(x => x.City.Equals("Levittown")).ShouldBeTrue();
        }

        [TestMethod]
        public void should_find_state_only()
        {
            var result = _sut.GetFromAddress("Pennsylvania").FirstOrDefault();
            result.State.ShouldEqual("Pennsylvania");
        }

        [TestMethod]
        public void should_find_stateabbr_only()
        {
            var result = _sut.GetFromAddress("PA").FirstOrDefault();
            result.State.ShouldEqual("Pennsylvania");
        }
    }
}
