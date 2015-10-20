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
       // Task<WeatherData> GetDataAsync(IWeatherRequest request);
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

        //public async Task<WeatherData> GetDataAsync(IWeatherRequest request)
        //{
        //    var requestUri = BuildCompleteRequest(request);

        //    var client = new WebClient();
        //    var task = await client.DownloadStringTaskAsync(requestUri);
        //    return await _parser.Parse(task);
        //}

        public WeatherData GetData(IWeatherRequest request)
        {
            var requestUri = BuildCompleteRequest(request);

            var client = new WebClient();
            var data = client.DownloadString(requestUri);
            var task = _parser.Parse(data);
            task.Wait(5000);
            return task.Result;
        }

        

        private Uri BuildCompleteRequest(IWeatherRequest request)
        {
            var builder = new UriBuilder(_config.BaseUri) {Query = request.Build()};
            return builder.Uri;
        }
    }

    
}