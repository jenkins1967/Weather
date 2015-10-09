using System;
using JimJenkins.GeoCoding.Services.Exceptions;
using JimJenkins.GeoCoding.Services.Parsing;
using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services
{
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
            catch (Exception err)
            {
                throw new GeoCodingParseException(err);
            }

            return parseResult;
        }


    }
}