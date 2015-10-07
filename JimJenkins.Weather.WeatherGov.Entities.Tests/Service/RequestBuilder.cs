using System;
using JimJenkins.Weather.WeatherGov.DataService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.Service
{
    [TestClass]
    public class RequestBuilder
    {
        
        [TestMethod]
        public void should_build_apparenttemp()
        {
            var requestParams = new RequestElementsProvider {ApparentTemperature = true};
            requestParams.Build().ShouldContain("appt");
        }
        
        [TestMethod]
        public void should_build_maxtemp()
        {
            var requestParams = new RequestElementsProvider { MaximumTemperature = true };
            requestParams.Build().ShouldContain("maxt");
        }

        [TestMethod]
        public void should_build_mintemp()
        {
            var requestParams = new RequestElementsProvider { MinimumTemperature = true };
            requestParams.Build().ShouldContain("mint");
        }

        [TestMethod]
        public void should_build_3HourTemp()
        {
            var requestParams = new RequestElementsProvider {ThreeHourlyTemperature = true};
            requestParams.Build().ShouldContain("temp");
        }

        [TestMethod]
        public void should_build_dewpoint()
        {
            var requestParams = new RequestElementsProvider { DewpointTemperature = true };
            requestParams.Build().ShouldContain("dew");
        }

        [TestMethod]
        public void should_build_12HourPop()
        {
            var requestParams = new RequestElementsProvider { TwelveHourProbabilityPrecipitation = true };
            requestParams.Build().ShouldContain("pop12");
        }

        [TestMethod]
        public void should_build_liquid_precip()
        {
            var requestParams = new RequestElementsProvider { LiquidPrecipitationAmount = true };
            requestParams.Build().ShouldContain("qpf");
        }

        [TestMethod]
        public void should_build_snowfall()
        {
            var requestParams = new RequestElementsProvider { SnowfallAmount = true };
            requestParams.Build().ShouldContain("snow");
        }

        [TestMethod]
        public void should_build_cloudcover()
        {
            var requestParams = new RequestElementsProvider { CloudCoverAmount = true };
            requestParams.Build().ShouldContain("sky");
        }

        [TestMethod]
        public void should_build_relative_humidity()
        {
            var requestParams = new RequestElementsProvider { RelativeHumidity = true };
            requestParams.Build().ShouldContain("rh");
        }

        [TestMethod]
        public void should_build_windspeed()
        {
            var requestParams = new RequestElementsProvider { WindSpeed = true };
            requestParams.Build().ShouldContain("wspd");
        }

        [TestMethod]
        public void should_build_winddirection()
        {
            var requestParams = new RequestElementsProvider { WindDirection = true };
            requestParams.Build().ShouldContain("wdir");
        }

        [TestMethod]
        public void should_build_weather()
        {
            var requestParams = new RequestElementsProvider { Weather = true };
            requestParams.Build().ShouldContain("wx");
        }

        [TestMethod]
        public void should_build_weathericons()
        {
            var requestParams = new RequestElementsProvider { WeatherIcons = true };
            requestParams.Build().ShouldContain("icons");
        }

        [TestMethod]
        public void should_build_waveheight()
        {
            var requestParams = new RequestElementsProvider { WaveHeight = true };
            requestParams.Build().ShouldContain("waveh");
        }

        [TestMethod]
        public void should_build_cycloneAbove34()
        {
            var requestParams = new RequestElementsProvider { ProbabilisticTropicalCycloneWindSpeedAbove34Knots = true };
            requestParams.Build().ShouldContain("cumw34");
        }

        [TestMethod]
        public void should_build_cycloneAbove50()
        {
            var requestParams = new RequestElementsProvider { ProbabilisticTropicalCycloneWindSpeedAbove50Knots = true };
            requestParams.Build().ShouldContain("cumw50");
        }

        [TestMethod]
        public void should_build_cycloneAbove64()
        {
            var requestParams = new RequestElementsProvider { ProbabilisticTropicalCycloneWindSpeedAbove64Knots = true };
            requestParams.Build().ShouldContain("cumw64");
        }

        [TestMethod]
        public void should_build_windgust()
        {
            var requestParams = new RequestElementsProvider { WindGust = true };
            requestParams.Build().ShouldContain("wgust");
        }

        [TestMethod]
        public void should_build_firewind()
        {
            var requestParams = new RequestElementsProvider { FireWeatherfromWind = true };
            requestParams.Build().ShouldContain("critfireo");
        }

        [TestMethod]
        public void should_build_firedry()
        {
            var requestParams = new RequestElementsProvider { FireWeatherfromDry = true };
            requestParams.Build().ShouldContain("dryfireo");
        }

        [TestMethod]
        public void should_build_probTornadoes()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofTornadoes = true };
            requestParams.Build().ShouldContain("ptornado");
        }

        [TestMethod]
        public void should_build_probHail()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofHail = true };
            requestParams.Build().ShouldContain("phail");
        }

        [TestMethod]
        public void should_build_probXtremeTornadoes()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofExtremeTornadoes = true };
            requestParams.Build().ShouldContain("pxtornado");
        }

        [TestMethod]
        public void should_build_probXtremeHail()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofExtremeHail = true };
            requestParams.Build().ShouldContain("pxhail");
        }

        [TestMethod]
        public void should_build_probTStormWinds()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofDamagingThunderstormWinds = true };
            requestParams.Build().ShouldContain("ptstmwinds");
        }

        [TestMethod]
        public void should_build_probXTremeTStormWinds()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofExtremeThunderstormWinds = true };
            requestParams.Build().ShouldContain("pxtstmwinds");
        }

        [TestMethod]
        public void should_build_probSevereTStorm()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofSevereThunderstorms = true };
            requestParams.Build().ShouldContain("ptotsvrtstm");
        }

        [TestMethod]
        public void should_build_probXTremeSevereTStorm()
        {
            var requestParams = new RequestElementsProvider { ProbabilityofExtremeSevereThunderstorms = true };
            requestParams.Build().ShouldContain("pxtotsvrtstm");
        }

        [TestMethod]
        public void should_build_watches_warning()
        {
            var requestParams = new RequestElementsProvider { WatchesWarningsandAdvisories = true };
            requestParams.Build().ShouldContain("wwa");
        }

        [TestMethod]
        public void should_build_ice_accumulation()
        {
            var requestParams = new RequestElementsProvider { IceAccumulation = true };
            requestParams.Build().ShouldContain("iceaccum");
        }
    }
}
