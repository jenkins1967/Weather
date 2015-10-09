using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JimJenkins.GeoCoding.Services.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JimJenkins.GeoCoding.Services.Exceptions;

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
            uri.AddToQueryString(string.Format("lat={0}&lon={1}", coordinate.Latitude, coordinate.Longitude));
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
            
            var resultTask = client.DownloadStringTaskAsync(uri);

            resultTask.Wait(10000); //10 seconds
            if (resultTask.Status == TaskStatus.RanToCompletion)
            {
                var data = resultTask.Result;
                var parseResult = _parser.Parse(data);

                return !parseResult.Status.Equals("ZERO_RESULTS")
                    ? _mapper.MapRequestResult(parseResult)
                    : new List<GeoCodingResult>();
            }

            throw new TimeoutException("Getting geocoding data timed out using " + uri.AbsoluteUri);
        }

    }
}