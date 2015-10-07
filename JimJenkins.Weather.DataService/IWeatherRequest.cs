using System;
using System.Collections.Generic;
using System.Linq;

namespace JimJenkins.Weather.WeatherGov.DataService
{
    public interface IWeatherRequest
    {
        RequestType RequestType { get; }
        IRequestLocationProvider Location { get; }

        DateTime? StartTimeUtc { get; set; }
        DateTime? EndTimeUtc { get; set; }

        UnitOfMeasure UnitOfMeasure { get; set; }

        IRequestElementsProvider DesiredElements { get; }

        string Build();
    }

    public class WeatherRequest : IWeatherRequest
    {
        public WeatherRequest(IRequestLocationProvider location, IRequestElementsProvider desiredElements)
        {
            DesiredElements = desiredElements;
            Location = location;
        }
        public RequestType RequestType { get; set; }
        public IRequestLocationProvider Location { get; private set; }
        public DateTime? StartTimeUtc { get; set; }
        public DateTime? EndTimeUtc { get; set; }
        public UnitOfMeasure UnitOfMeasure { get; set; }
        public IRequestElementsProvider DesiredElements { get; private set; }

        public string Build()
        {
            const string format = "{0}={1}";

            if (DesiredElements == null)
            {
                throw new InvalidOperationException("Parameters is null");
            }

            var parameters = DesiredElements.Build();
            var location = Location.Build();

            var list = new List<string>
            {
                string.Format(format, "product", RequestType == RequestType.TimeSeries ? "time-series" : "glance"),
                string.Format(format, "Unit", UnitOfMeasure == UnitOfMeasure.English ? "e" : "m")
            };


            if (StartTimeUtc.HasValue)
            {
                list.Add(string.Format(format, "begin", StartTimeUtc.Value.ToString("s")));
            }
            if (EndTimeUtc.HasValue)
            {
                list.Add(string.Format(format, "end", EndTimeUtc.Value.ToString("s")));
            }
            
            list.AddRange(parameters.Select(p => string.Format("{0}={0}", p)));
            list.AddRange(location.Select(l => string.Format(format, l.Key, l.Value)));

            return string.Join("&", list.ToArray());
        }
    }
}