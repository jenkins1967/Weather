using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.ExceptionServices;
using System.Security.Cryptography;
using System.Threading.Tasks;
using JimJenkins.GeoCoding.Services.Parsing;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JimJenkins.GeoCoding.Services
{
    public interface IGeoCodingService
    {
        GeoCodingResult GetFromZip(string zip);
        GeoCodingResult GetFromCoordinate(Coordinate coordinate);

        GeoCodingResult GetFromCityState(string city, string state);
    }

    public interface IGeoCodingParser
    {
        GeoCodingResult Parse(string data);
    }

    public class GeoCodingParser : IGeoCodingParser
    {
        public GeoCodingResult Parse(string json)
        {
            var result = new GeoCodingResult();
            var parseResult = JsonConvert.DeserializeObject<RequestResult>(json);
           
            return null;
        }
        
    }

    public class GeoCodingService : IGeoCodingService
    {
        private readonly IGeoCodingServiceConfigProvider _configProvider;
        private readonly IGeoCodingParser _parser;


        public GeoCodingService(IGeoCodingServiceConfigProvider configProvider,IGeoCodingParser parser)
        {
            _parser = parser;
            _configProvider = configProvider;
        }

        public GeoCodingResult GetFromZip(string zip)
        {
            var uri = new UriBuilder(_configProvider.BaseUri);
            uri.Query = uri.Query + string.Format("address={0}" + zip);

            return GetData(uri.Uri);


        }
    
        public GeoCodingResult GetFromCoordinate(Coordinate coordinate)
        {
            var uri = new UriBuilder(_configProvider.BaseUri);
            var address = string.Format("lat={0}&lon={1}", coordinate.Latitude,coordinate.Longitude);
            uri.Query = uri.Query + address;

            return GetData(uri.Uri);    
        }

        public GeoCodingResult GetFromCityState(string city, string state)
        {
            var uri = new UriBuilder(_configProvider.BaseUri);
            var address = string.Format("{0},{1}", city, state);

            uri.Query = uri.Query + string.Format("address={0}" + Uri.EscapeDataString(address));

            return GetData(uri.Uri);   
        }

        private GeoCodingResult GetData(Uri uri)
        {
            var client = new WebClient();
            var resultTask = client.DownloadStringTaskAsync(uri);

            resultTask.Wait();

            var data = resultTask.Result;

            return _parser.Parse(data);
        }

    }
}