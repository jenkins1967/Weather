using System;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Should;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.DataParsing
{
    public abstract class BaseParseTimeSeriesUnsummarizedData
    {
        protected WeatherData _sut;
        protected string _xmlData;
        public BaseParseTimeSeriesUnsummarizedData(string fileLocation)
        {
            _xmlData = File.ReadAllText(fileLocation);
            var serializer = new XmlSerializer(typeof(WeatherData));
            using (var reader = XmlReader.Create(new StringReader(_xmlData)))
            {
                _sut = (WeatherData)serializer.Deserialize(reader);
            }
        }

        protected void ValidateBaseParameter<TValue>(WeatherValue<TValue> param)
        {
            param.Name.ShouldNotBeEmpty();
            param.Values.Length.ShouldBeGreaterThan(0);
            param.Units.ShouldNotBeEmpty();
            param.TimeLayoutKey.ShouldNotBeEmpty();
            param.Type.ShouldNotBeEmpty();
            _sut.Data.TimeLayouts.Any(x => x.Key.Equals(param.TimeLayoutKey)).ShouldBeTrue();
        }

    }

    [TestClass]
    public class ParseTimeSeriesUnsummarizedDataParameterSpecs:BaseParseTimeSeriesUnsummarizedData
    {
        public ParseTimeSeriesUnsummarizedDataParameterSpecs()
            : base("SampleXml/TimeSeriesUnsummarized.xml")
        {
           
        }

        [TestMethod]
        public void should_populate_hazards_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].Hazards)
            {
                param.Name.ShouldNotBeEmpty();
                param.TimeLayoutKey.ShouldNotBeEmpty();
                _sut.Data.TimeLayouts.Any(x => x.Key.Equals(param.TimeLayoutKey)).ShouldBeTrue();
                param.HazardConditions.Hazard.ShouldNotBeNull();
                param.HazardConditions.Hazard.Length.ShouldBeGreaterThan(0);

            }
        }

        [TestMethod]
        public void should_populate_hazard_detail_correctly()
        {
            foreach (var hazard in _sut.Data.Parameters[0].Hazards[0].HazardConditions.Hazard)
            {
                hazard.Code.ShouldNotBeEmpty();
                hazard.Phenomena.ShouldNotBeEmpty();
                hazard.Significance.ShouldNotBeEmpty();
                hazard.Type.ShouldNotBeEmpty();
                hazard.TextUrl.ShouldNotBeEmpty();
            }
        }

        [TestMethod]
        public void should_populate_temperatures_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].Temperatures)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_precipitations_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].Precipitations)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_humidity_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].Humidities)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_prob_of_precipitations_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].ProbabilityOfPrecipitations)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_windspeeds_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].WindSpeeds)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_winddirection_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].WindDirections)
            {
                ValidateBaseParameter(param);
            }
        }

        [TestMethod]
        public void should_populate_cloudamount_correctly()
        {
            foreach (var param in _sut.Data.Parameters[0].CloudAmounts)
            {
                ValidateBaseParameter(param);
            }
        }


        [TestMethod]
        public void should_populate_time_layouts()
        {
            foreach (var time in _sut.Data.TimeLayouts)
            {
                time.Key.ShouldNotBeEmpty();
                time.Summarization.ShouldNotBeEmpty();
                time.TimeCoordinate.ShouldNotBeEmpty();
                time.StartTimes.Length.ShouldBeGreaterThan(0);
            }
        }

        [TestMethod]
        public void should_populate_locations()
        {
            foreach (var location in _sut.Data.Locations)
            {
                location.Key.ShouldNotBeEmpty();
                location.Point.ShouldNotBeNull();
                location.Point.Latitude.ShouldNotEqual(0);
                location.Point.Longitude.ShouldNotEqual(0);
            }
        }

        [TestMethod]
        public void should_populate_moreinformations()
        {
            foreach (var more in _sut.Data.MoreInformations)
            {
                more.LocationKey.ShouldNotBeEmpty();
                _sut.Data.Locations
                    .Any(x => x.Key.Equals(more.LocationKey))
                    .ShouldBeTrue();
            }
        }

        [TestMethod]
        public void should_populate_source_fields()
        {
            var source = _sut.Head.Source;
            source.Credit.ShouldNotBeEmpty();
            source.CreditLogo.ShouldNotBeEmpty();
            source.Disclaimer.ShouldNotBeEmpty();
            source.Feedback.ShouldNotBeEmpty();
            source.MoreInformation.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void should_populate_product_fields()
        {
            var prod = _sut.Head.Product;
            prod.Category.ShouldNotBeEmpty();
            prod.CreationDate.ShouldBeGreaterThan<DateTime>(new DateTime());
            prod.Field.ShouldNotBeEmpty();
            prod.Title.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void should_populate_weather_correctly()
        {
            var weather = _sut.Data.Parameters[0].Weathers[0];
            weather.Name.ShouldNotBeEmpty();
            weather.TimeLayoutKey.ShouldNotBeEmpty();
            _sut.Data.TimeLayouts.Any(x => x.Key.Equals(weather.TimeLayoutKey)).ShouldBeTrue();
            weather.WeatherConditions.Length.ShouldBeGreaterThan(0);
            foreach (var condition in weather.WeatherConditions[0].Values)
            {
                condition.Coverage.ShouldNotBeEmpty();
                condition.Intensity.ShouldNotBeEmpty();
                condition.Qualifier.ShouldNotBeEmpty();
                condition.WeatherType.ShouldNotBeEmpty();
            }

            var values = _sut.Data.Parameters[0].Weathers[0].WeatherConditions.First(x => x.Values.Length > 1).Values;
            values[1].Additive.ShouldNotBeEmpty();


        }
    }
    [TestClass]
    public class ParseTimeSeriesUnsummarizedDataTopLevelObjectsSpecs:BaseParseTimeSeriesUnsummarizedData
    {

        public ParseTimeSeriesUnsummarizedDataTopLevelObjectsSpecs()
            : base("SampleXml/TimeSeriesUnsummarized.xml")
        {
           
        }

        
        [TestMethod]
        public void should_populate_head()
        {
            _sut.Head.ShouldNotBeNull();
            _sut.Head.Source.ShouldNotBeNull();
            _sut.Head.Product.ShouldNotBeNull();
        }

        [TestMethod]
        public void should_populate_data()
        {
            _sut.Data.ShouldNotBeNull();
            _sut.Data.Locations.Length.ShouldBeGreaterThan(0);
            _sut.Data.MoreInformations.Length.ShouldBeGreaterThan(0);
            _sut.Data.TimeLayouts.Length.ShouldBeGreaterThan(0);
            
        }

        [TestMethod]
        public void should_populate_parameters()
        {
            _sut.Data.Parameters.ShouldNotBeNull();
            _sut.Data.Parameters.Length.ShouldBeGreaterThan(0);
            foreach (var param in _sut.Data.Parameters)
            {
                param.Temperatures.Length.ShouldBeGreaterThan(0);
                param.WindSpeeds.Length.ShouldBeGreaterThan(0);
                param.WindDirections.Length.ShouldBeGreaterThan(0);
                param.CloudAmounts.Length.ShouldBeGreaterThan(0);
                param.Precipitations.Length.ShouldBeGreaterThan(0);
                param.ProbabilityOfPrecipitations.Length.ShouldBeGreaterThan(0);
                param.Humidities.Length.ShouldBeGreaterThan(0);
                param.Hazards.Length.ShouldBeGreaterThan(0);
                param.Weathers.Length.ShouldBeGreaterThan(0);
            }
        }
       
    }

    [TestClass]
    public class WeatherIconParsingSpecs : BaseParseTimeSeriesUnsummarizedData
    {
        public WeatherIconParsingSpecs():base("SampleXml/WeatherIcons.xml")
        {

        }

        [TestMethod]
        public void should_populate_icons_object()
        {
            _sut.Data.Parameters[0].Icons.ShouldNotBeNull();
            _sut.Data.Parameters[0].Icons.Name.ShouldNotBeEmpty();
            _sut.Data.Parameters[0].Icons.Type.ShouldNotBeEmpty();
            _sut.Data.Parameters[0].Icons.TimeLayoutKey.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void should_populate_icon_links()
        {
            foreach(var link in _sut.Data.Parameters[0].Icons.Link)
            {
                link.ShouldNotBeEmpty();
                Uri result;
                Uri.TryCreate(link, UriKind.Absolute, out result).ShouldBeTrue();
            }
        }
    }

    [TestClass]
    public class WaterStateParsingSpecs : BaseParseTimeSeriesUnsummarizedData
    {
        public WaterStateParsingSpecs()
            : base("SampleXml/WaterState.xml")
        {

        }

        [TestMethod]
        public void should_populate_waterstate_object()
        {
            _sut.Data.Parameters[0].WaterState.ShouldNotBeNull();
            _sut.Data.Parameters[0].WaterState.TimeLayoutKey.ShouldNotBeEmpty();
        }

        [TestMethod]
        public void should_populate_waves_correctly()
        {
            _sut.Data.Parameters[0].WaterState.Waves.Name.ShouldNotBeEmpty();
            _sut.Data.Parameters[0].WaterState.Waves.Units.ShouldNotBeEmpty();
            _sut.Data.Parameters[0].WaterState.Waves.Type.ShouldNotBeEmpty();
            _sut.Data.Parameters[0].WaterState.Waves.Values[0].ShouldEqual<float?>(1f);
            _sut.Data.Parameters[0].WaterState.Waves.Values[1].ShouldEqual<float?>(15.4F);
        }
    }

    [TestClass]
    public class ConvectiveHazardsSpecs : BaseParseTimeSeriesUnsummarizedData
    {
        public ConvectiveHazardsSpecs():base("SampleXml/ConvectiveHazard.xml")
        {

        }

        [TestMethod]
        public void should_populate_convective_objectcs()
        {
            _sut.Data.Parameters[0].ConvectiveHazard[0].SevereComponent.Type.ShouldEqual("tornadoes");
            _sut.Data.Parameters[0].ConvectiveHazard[1].SevereComponent.Type.ShouldEqual("hail");
            _sut.Data.Parameters[0].ConvectiveHazard[2].SevereComponent.Type.ShouldEqual("severe thunderstorms");
        }
    }

    [TestClass]
    public class LoadEverythingFile : BaseParseTimeSeriesUnsummarizedData
    {
        public LoadEverythingFile():base("SampleXml/Everything.xml")
        {

        }

        [TestMethod]
        public void should_have_loaded()
        {
            _sut.Data.ShouldNotBeNull();
            _sut.Head.ShouldNotBeNull();
        }
    }
}
