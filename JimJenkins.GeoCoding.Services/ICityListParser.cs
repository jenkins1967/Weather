using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

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
            Task<IEnumerable<Coordinate>> coordinateTask = null;
            Task<IEnumerable<string>> cityNameTask = null;
            IList<Task> readerTasks = new List<Task>();
            var settings = new XmlReaderSettings {Async = true};
            using (var parser = XmlReader.Create(new StringReader(data), settings))
            {
                while (parser.Read())
                {
                    if (parser.NodeType == XmlNodeType.Element)
                    {
                        if (parser.LocalName.Equals("latLonList"))
                        {
                            readerTasks.Add(parser.ReadElementContentAsStringAsync().ContinueWith(task =>
                            {
                                coordinateTask = new Task<IEnumerable<Coordinate>>(() => ParseCoordinates(task.Result));
                                coordinateTask.Start();
                            }));

                        }
                        if(parser.LocalName.Equals("cityNameList"))
                        {
                            readerTasks.Add(parser.ReadElementContentAsStringAsync().ContinueWith(task =>
                            {
                                cityNameTask = new Task<IEnumerable<string>>(() => ParseCityNames(task.Result));
                                cityNameTask.Start();
                            }));
                        }
                    }
                }

                Task.WaitAll(readerTasks.ToArray());
                parser.Close();
            }

            if (coordinateTask == null || cityNameTask == null)
            {
                throw new Exception("The city name provider did not return correct data. ");
            }
            Task.WaitAll(new Task[] {coordinateTask, cityNameTask});

            return BuildCityList(coordinateTask.Result.ToList(), cityNameTask.Result.ToList());
          //  return new List<City>();

        }

        private IEnumerable<City> BuildCityList(IList<Coordinate> coordinates, IEnumerable<string> cityNames)
        {
            var cityList = cityNames.ToArray();
            var cities = new City[cityList.Length];

            for (var index = 0; index < cities.Length; index++)
            {
                cities[index] = new City {GeoCoordinates = coordinates[index], Name = cityList[index]};
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