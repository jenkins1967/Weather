using System;
using System.Linq;
using JimJenkins.Weather.WeatherGov.DataService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.Service
{
    [TestClass]
    public class LocationProviders
    {
        [TestMethod]
        public void should_return_lat_and_lon()
        {
            var sut = new CoordinateRequestLocationProvider(40.344F, -74.58F);
            var args = sut.Build();

            args.Count(x => x.Key.Equals("lat") && x.Value.Equals("40.344")).ShouldEqual(1);
            args.Count(x => x.Key.Equals("lon") && x.Value.Equals("-74.58")).ShouldEqual(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void should_reject_bad_lat()
        {
            var sut = new CoordinateRequestLocationProvider(-200, -74.85F);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void should_reject_bad_lon()
        {
            var sut = new CoordinateRequestLocationProvider(40.3f, -374.85F);
        }
    }
}
