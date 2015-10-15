using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using JimJenkins.GeoCoding.Services.Extensions;

namespace JimJenkins.GeoCoding.Services
{
    public interface IGeoCodingService
    {
        GeoCodingResult GetFromZip(string zip);
        GeoCodingResult GetFromCoordinate(Coordinate coordinate);

        GeoCodingResult GetFromCityState(string city, string state);

        IEnumerable<GeoCodingResult> GetFromAddress(string address);
    }


    public class GeoCodingService : IGeoCodingService
    {
        private readonly IGeoCodingServiceConfigProvider _configProvider;
        private readonly IGeoCodingParser _parser;
        private readonly IGeoCodingDataMapper _mapper;


        public GeoCodingService(IGeoCodingServiceConfigProvider configProvider,
            IGeoCodingParser parser,
            IGeoCodingDataMapper mapper)
        {
            _mapper = mapper;
            _parser = parser;
            _configProvider = configProvider;
        }

        public GeoCodingResult GetFromZip(string zip)
        {
            return GetFromAddress(zip).FirstOrDefault();
        }
    
        public GeoCodingResult GetFromCoordinate(Coordinate coordinate)
        {
            var uri = new UriBuilder(_configProvider.BaseUri);
            uri.AddToQueryString(string.Format("latlng={0},{1}", coordinate.Latitude, coordinate.Longitude));
            return GetData(uri.Uri).FirstOrDefault();    
        }

        public GeoCodingResult GetFromCityState(string city, string state)
        {
            return GetFromAddress(string.Format("{0},{1}", city, state)).FirstOrDefault();
        }

        public IEnumerable<GeoCodingResult> GetFromAddress(string address)
        {
            var uri = new UriBuilder(_configProvider.BaseUri);
            uri.AddToQueryString(string.Format("address={0}", 
                Uri.EscapeDataString(address)));
                
            return GetData(uri.Uri);   
        }

        private IEnumerable<GeoCodingResult> GetData(Uri uri)
        {     

        var client = new WebClient();
            
            var data = client.DownloadString(uri);
                          
            var parseResult = _parser.Parse(data);

            return !parseResult.Status.Equals("ZERO_RESULTS")
                ? _mapper.MapRequestResult(parseResult)
                : new List<GeoCodingResult>();
            
        }
    }


 

}