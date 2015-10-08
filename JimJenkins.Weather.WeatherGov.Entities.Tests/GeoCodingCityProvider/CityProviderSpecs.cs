using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JimJenkins.GeoCoding.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JimJenkins.Weather.WeatherGov.Entities.Tests.GeoCodingCityProvider
{
    [TestClass]
    public class CityProviderSpecs
    {
        private ICityListProvider _sut;

        public CityProviderSpecs()
        {
            _sut = new CityListProvider(new CityListProviderConfiguration(), new CityListParser());
        }
        [TestMethod]
        public void should_return_allcities()
        {
            var result = _sut.GetAllCities();
        }

        
    }
}
