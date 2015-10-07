using System;
using System.IO;
using System.Linq;
using JimJenkins.GeoCoding.Services;
using JimJenkins.GeoCoding.Services.Parsing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.DataParsing
{
    
    [TestClass]
    public class GeoCodingParsedObjectZipCodeSpecs
    {
        private RequestResult _sut;
        public GeoCodingParsedObjectZipCodeSpecs()
        {
            var data = File.ReadAllText("SampleJson/zipcode_lookup.json");
            _sut = JsonConvert.DeserializeObject<RequestResult>(data);
        }
        [TestMethod]
        public void should_load_address_components()
        {
            var components = _sut.Result[0].AddressComponents;
            components[0].LongName.ShouldEqual("08610");
            components[0].ShortName.ShouldEqual("08610");
            components[0].Types[0].ShouldEqual("postal_code");

            components[1].LongName.ShouldEqual("Trenton");
            components[1].ShortName.ShouldEqual("Trenton");
            components[1].Types[0].ShouldEqual("locality");
            components[1].Types[1].ShouldEqual("political");

            components[2].LongName.ShouldEqual("New Jersey");
            components[2].ShortName.ShouldEqual("NJ");
            components[2].Types[0].ShouldEqual("administrative_area_level_1");
            components[2].Types[1].ShouldEqual("political");

            components[3].LongName.ShouldEqual("United States");
            components[3].ShortName.ShouldEqual("US");
            components[3].Types[0].ShouldEqual("country");
            components[3].Types[1].ShouldEqual("political");

        }

        [TestMethod]
        public void should_load_formatted_address()
        {
            _sut.Result[0].FormattedAddress.ShouldEqual("Trenton, NJ 08610, USA");
        }

        [TestMethod]
        public void should_load_boundaries()
        {
            var bounds = _sut.Result[0].Geometry.Bounds;
            bounds.ShouldNotBeNull();
            bounds.UpperLeft.Latitude.ShouldEqual(40.223088f);
            bounds.UpperLeft.Longitude.ShouldEqual(-74.670079f);
            bounds.LowerRight.Latitude.ShouldEqual(40.175044f);
            bounds.LowerRight.Longitude.ShouldEqual(-74.752841f);

        }

        [TestMethod]
        public void should_load_location()
        {
            _sut.Result[0].Geometry.Location.Latitude.ShouldEqual(40.1956132f);
            _sut.Result[0].Geometry.Location.Longitude.ShouldEqual(-74.71614889999999f);
        }

        [TestMethod]
        public void should_load_placeid()
        {
            _sut.Result[0].PlaceId.ShouldEqual("ChIJ7SJOJWFZwYkRndzlVZifYFY");
        }

        [TestMethod]
        public void should_load_postcode_locals()
        {
            _sut.Result[0].PostCodeLocalities[0].ShouldEqual("Hamilton Township");
            _sut.Result[0].PostCodeLocalities[1].ShouldEqual("Trenton");
        }

        [TestMethod]
        public void should_load_types()
        {
            _sut.Result[0].Types[0].ShouldEqual("postal_code");
        }
    }
}
