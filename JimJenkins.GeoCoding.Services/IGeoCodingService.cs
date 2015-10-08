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
using JimJenkins.GeoCoding.Services.Exceptions;

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
        RequestResult Parse(string data);
    }

    public class GeoCodingParser : IGeoCodingParser
    {
        public RequestResult Parse(string json)
        {
            var result = new GeoCodingResult();
            RequestResult parseResult;
            try
            {
                parseResult = JsonConvert.DeserializeObject<RequestResult>(json);
            }
            catch(Exception err)
            {
                throw new GeoCodingParseException(err);
            }

            return parseResult;
        }
        
        
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
            var parseResult = _parser.Parse(data);

            return _mapper.MapRequestResult(parseResult);
        }

    }

    public interface IGeoCodingDataMapper
    {
        GeoCodingResult MapRequestResult(RequestResult requestResult);
    }

    public class GeoCodingDataMapper:IGeoCodingDataMapper
    {
        public GeoCodingResult MapRequestResult(RequestResult requestResult)
        {
            try
            {


                var resultObj = requestResult.Result[0];
                var geoCodingResult = new GeoCodingResult
                {
                    Coordinate = new Coordinate(resultObj.Geometry.Location.Latitude, resultObj.Geometry.Location.Longitude)
                };

                var city = resultObj.AddressComponents.FirstOrDefault(x => x.IsCity);
                geoCodingResult.City = (city != null) ? city.LongName : string.Empty;   
             
                var state = resultObj.AddressComponents.FirstOrDefault(x => x.IsState);
                geoCodingResult.State = (state != null) ? state.LongName : string.Empty;
                
                var postalCode = resultObj.AddressComponents.FirstOrDefault(x => x.IsPostalCode);
                geoCodingResult.Zip = (postalCode != null) ? postalCode.LongName : string.Empty;
                
                var country = resultObj.AddressComponents.FirstOrDefault(x => x.IsCountry);
                geoCodingResult.Country = (country != null) ? country.LongName : string.Empty;
                
                return geoCodingResult;
            }
            catch (Exception err)
            {
                throw new GeoCodingMapException(err);
            }
        }

    }
}