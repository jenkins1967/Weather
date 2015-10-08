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
            var result = new RequestResult();
            //act
            var codedResult = _sut.MapRequestResult(result);

            //assert
            codedResult.City.ShouldNotBeEmpty();
            codedResult.State.ShouldNotBeEmpty();
            codedResult.Country.ShouldNotBeEmpty();
            codedResult.Zip.ShouldBeEmpty();
            codedResult.Coordinate.Latitude.ShouldEqual(0f);
            codedResult.Coordinate.Longitude.ShouldEqual(0f);

       } 

        [TestMethod]
        public void should_map_city_request()
        {
            //arrange
            var result = new RequestResult();
            //act
            var codedResult = _sut.MapRequestResult(result);

            //assert
            codedResult.City.ShouldNotBeEmpty();
            codedResult.State.ShouldNotBeEmpty();
            codedResult.Zip.ShouldNotBeEmpty();
            codedResult.Country.ShouldNotBeEmpty();
            codedResult.Coordinate.Latitude.ShouldEqual(0f);
            codedResult.Coordinate.Longitude.ShouldEqual(0f);
        }
    }
}
