using System.IO;
using JimJenkins.GeoCoding.Services;
using JimJenkins.GeoCoding.Services.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.GeoCodingMapping
{
    [TestClass]
    public class MapperSpecs
    {
        private IGeoCodingDataMapper _sut;

        public MapperSpecs()
        {
            _sut = new GeoCodingDataMapper();            
        }

        [TestMethod]
        public void should_map_zip_request()
        {
            //arrange
            var result = GetZipRequest();
            //act
            var codedResult = _sut.MapRequestResult(result);

            //assert
            codedResult.City.ShouldNotBeEmpty();
            codedResult.State.ShouldNotBeEmpty();
            codedResult.Country.ShouldNotBeEmpty();
            codedResult.Zip.ShouldNotBeEmpty();
            codedResult.Coordinate.Latitude.ShouldEqual(40.1956139f);
            codedResult.Coordinate.Longitude.ShouldEqual(-74.71615f);

       } 

        [TestMethod]
        public void should_map_city_request()
        {
            //arrange
            var result = GetCityRequest();
            //act
            var codedResult = _sut.MapRequestResult(result);

            //assert
            codedResult.City.ShouldNotBeEmpty();
            codedResult.State.ShouldNotBeEmpty();
            codedResult.Zip.ShouldBeEmpty();
            codedResult.Country.ShouldNotBeEmpty();
            codedResult.Coordinate.Latitude.ShouldEqual(40.2170525f);
            codedResult.Coordinate.Longitude.ShouldEqual(-74.7429352f);
        }

        private RequestResult GetZipRequest()
        {
            var data = File.ReadAllText("SampleJson/zipcode_lookup.json");
            var parser = new GeoCodingParser();
            return parser.Parse(data);
        }

        private RequestResult GetCityRequest()
        {
            var data = File.ReadAllText("SampleJson/city_lookup.json");
            var parser = new GeoCodingParser();
            return parser.Parse(data);
        }
    }
}
