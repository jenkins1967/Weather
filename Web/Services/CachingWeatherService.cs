using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Web;
using JimJenkins.Weather.WeatherGov.DataService;
using JimJenkins.Weather.WeatherGov.Entities;

namespace Web.Services
{
    public class CachingWeatherService : CachingDecorator, IWeatherService
    {
        private readonly IWeatherService _decorated;

        public CachingWeatherService(IWeatherService decorated, System.Runtime.Caching.ObjectCache cache) : base(cache)
        {
            _decorated = decorated;
        }

        public WeatherData GetData(IWeatherRequest request)
        {
            var location = string.Join(",",
                request.Location.Build().Select(x => string.Format("{0}{1}", x.Key, x.Value)).ToArray());

            var times = string.Format("Start:{0}End:{1}",
                request.StartTimeUtc.HasValue ? request.StartTimeUtc.Value.ToLongDateString() : string.Empty,
                request.EndTimeUtc.HasValue ? request.EndTimeUtc.Value.ToLongDateString() : string.Empty);

            var key = MakeKey(location + times);
            return Get(key, () => _decorated.GetData(request));
        }

        public override CacheItemPolicy Policy
        {
            get
            {
                var policy = new CacheItemPolicy
                {
                    AbsoluteExpiration = new DateTimeOffset(DateTime.UtcNow.AddHours(1))
                };
                return policy;
            }
        }

        public override string CacheKeyBase
        {
            get { return "CachingWeatherService"; }
        }
    }


}