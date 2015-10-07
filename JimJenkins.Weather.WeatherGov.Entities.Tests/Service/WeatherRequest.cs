using System;
using System.Runtime.InteropServices;
using JimJenkins.Weather.WeatherGov.DataService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.Service
{
    [TestClass]
    public class WeatherRequestSpecs
    {
        private WeatherRequest _sut;

        [TestInitialize]
        public void Initialize()
        {
            _sut = new WeatherRequest(
                new CoordinateRequestLocationProvider(40.34F, -74.123f),
                new RequestElementsProvider {MaximumTemperature = true});

        }
        [TestMethod]
        public void should_return_start_date()
        {
            var date = DateTime.Now.ToUniversalTime();
            _sut.StartTimeUtc = date;
            _sut.Build().ShouldContain("begin=" + date.ToString("s"));
        }

        [TestMethod]
        public void should_return_end_date()
        {
            var date = DateTime.Now.ToUniversalTime();
            _sut.EndTimeUtc = date;
            _sut.Build().ShouldContain("end=" + date.ToString("s"));
        }

        [TestMethod]
        public void should_not_return_start_date()
        {           
            _sut.Build().ShouldNotContain("begin=");
        }

        [TestMethod]
        public void should_not_return_end_date()
        {
            _sut.Build().ShouldNotContain("end=");
        }

        [TestMethod]
        public void should_return_english_system()
        {
            _sut.UnitOfMeasure = UnitOfMeasure.English;
            _sut.Build().ShouldContain("Unit=e");
        }

        [TestMethod]
        public void should_return_metric_system()
        {
            _sut.UnitOfMeasure = UnitOfMeasure.Metric;
            _sut.Build().ShouldContain("Unit=m");
        }

        [TestMethod]
        public void should_return_product_timeseries()
        {
            _sut.RequestType = RequestType.TimeSeries;
            _sut.Build().ShouldContain("product=time-series");
        }

        [TestMethod]
        public void should_return_product_glance()
        {
            _sut.RequestType = RequestType.Glance;
            _sut.Build().ShouldContain("product=glance");
        }

        [TestMethod]
        public void should_include_location()
        {
            _sut.Build().ShouldContain("lat=");
            _sut.Build().ShouldContain("lon=");
        }
        [TestMethod]
        public void should_include_parameters()
        {
            _sut.Build().ShouldContain("maxt=");
        }
    }
}
