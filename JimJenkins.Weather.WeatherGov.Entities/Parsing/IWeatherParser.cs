using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace JimJenkins.Weather.WeatherGov.Entities.Parsing
{
    public interface IWeatherParser
    {
        Task<WeatherData> Parse(string xml);
    }

    public class WeatherParser : IWeatherParser
    {
        public Task<WeatherData> Parse(string xml)
        {
            var serializer = new XmlSerializer(typeof(WeatherData));

            var task = new Task<WeatherData>(() =>
            {
                using (var reader = XmlReader.Create(new StringReader(xml)))
                {
                    return (WeatherData)serializer.Deserialize(reader);
                }
            });

            task.Start();
            return task;
        }
    }
}