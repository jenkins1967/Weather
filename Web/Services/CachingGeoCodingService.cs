using System.Collections.Generic;
using System.Runtime.InteropServices;
using JimJenkins.GeoCoding.Services;
using Web.Services;

namespace Web.Services
{

    public class CachingGeoCodingService:CachingDecorator,IGeoCodingService
    {
        private readonly IGeoCodingService _decoratedService;
        
        public CachingGeoCodingService(IGeoCodingService decoratedService, System.Runtime.Caching.ObjectCache cache):base(cache)
        {
            _decoratedService = decoratedService;
            
        }

        public GeoCodingResult GetFromZip(string zip)
        {
            var key = MakeKey(zip);
            return Get(key, () => _decoratedService.GetFromZip(zip));
        }

        public GeoCodingResult GetFromCoordinate(Coordinate coordinate)
        {
            var key = MakeKey(coordinate);
            return Get(key, () => _decoratedService.GetFromCoordinate(coordinate));
        }

        public GeoCodingResult GetFromCityState(string city, string state)
        {
            var key =  MakeKey(city + state);
            return Get(key, () => _decoratedService.GetFromCityState(city, state));
        }

        public IEnumerable<GeoCodingResult> GetFromAddress(string address)
        {
            var key = MakeKey(address);
            return Get(key, () => _decoratedService.GetFromAddress(address));
        }


        public override string CacheKeyBase
        {
            get { return "CachingGeoCodingService"; }
        }
    }
}