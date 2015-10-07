using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using JimJenkins.Weather.WeatherGov.Entities;
using JimJenkins.Weather.WeatherGov.Entities.Parsing;

namespace JimJenkins.Weather.WeatherGov.DataService
{
    public interface IWeatherService
    {
        Task<WeatherData> GetDataAsync(IWeatherRequest request);
        WeatherData GetData(IWeatherRequest request);

    }

    public class WeatherService : IWeatherService
    {
        private readonly IWeatherServiceConfiguration _config;
        private readonly IWeatherParser _parser;

        public WeatherService(IWeatherServiceConfiguration config, 
            IWeatherParser weatherParser)
        {
            _config = config;
            _parser = weatherParser;
        }

        public async Task<WeatherData> GetDataAsync(IWeatherRequest request)
        {
            var requestUri = BuildCompleteRequest(request);

            var client = new WebClient();
            var task = await client.DownloadStringTaskAsync(requestUri);
            return await _parser.Parse(task);
        }

        public WeatherData GetData(IWeatherRequest request)
        {
            var result = GetDataAsync(request);
            result.Wait();
            return result.Result;
        }


        private Uri BuildCompleteRequest(IWeatherRequest request)
        {
            var builder = new UriBuilder(_config.BaseUri) {Query = request.Build()};
            return builder.Uri;
        }
    }

    
}