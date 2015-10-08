using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;

namespace JimJenkins.GeoCoding.Services
{
    public interface ICityListParser
    {
        IEnumerable<City> Parse(string data);
    }

    public class CityListParser : ICityListParser
    {
        public IEnumerable<City> Parse(string data)
        {
            var latLonList = string.Empty;
            var cityNameList = string.Empty;
            Task<IEnumerable<Coordinate>> coordinateTask;
            Task<IEnumerable<string>> cityNameTask;

            using (var parser = XmlReader.Create(new StringReader(data)))
            {
                parser.MoveToContent();
                parser.MoveToElement();
                latLonList = parser.ReadContentAsString();
                coordinateTask = new Task<IEnumerable<Coordinate>>(() => ParseCoordinates(latLonList));

                parser.MoveToElement();
                cityNameList = parser.ReadContentAsString();
                cityNameTask = new Task<IEnumerable<string>>(() => ParseCityNames(cityNameList));
                parser.Close();
            }

            Task.WaitAll(new Task[] {coordinateTask, cityNameTask});

            return BuildCityList(coordinateTask.Result.ToList(), cityNameTask.Result.ToList());

        }

        private IEnumerable<City> BuildCityList(IList<Coordinate> coordinates, IEnumerable<string> cityNames)
        {
            var cityList = cityNames.ToArray();
            var cities = new City[cityList.Length];

            for (var index = 0; index < cities.Length; index++)
            {
                cities[0] = new City {GeoCoordinates = coordinates[index], Name = cityList[index]};
            }

            return cities;
        }

        private IEnumerable<Coordinate> ParseCoordinates(string data)
        {
            var pairs = data.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            var coordinates = pairs.ToList().Select(SplitCoordinates);
            return coordinates;
        }

        private Coordinate SplitCoordinates(string data)
        {
            var pairs = data.Split(",".ToCharArray());
            return new Coordinate (float.Parse(pairs[0]), float.Parse(pairs[1]));
        }

        private IEnumerable<string> ParseCityNames(string data)
        {
            return data.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
        }
    }
}