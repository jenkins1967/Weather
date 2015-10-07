using System;

namespace JimJenkins.GeoCoding.Services
{
    public interface IGeoCodingServiceConfigProvider
    {
        Uri BaseUri { get; set; }
        
    }

    public class GeoCodingServiceConfigProvider : IGeoCodingServiceConfigProvider
    {
        public GeoCodingServiceConfigProvider(string serviceKey)
        {
            if (string.IsNullOrEmpty(serviceKey))
            {
                throw new ArgumentNullException("serviceKey");
            }
            BaseUri =
                new Uri("https://maps.googleapis.com/maps/api/geocode/json?key=" + serviceKey);

        //https://maps.googleapis.com/maps/api/geocode/json?address=19055&key=AIzaSyDrDkd4cYr21RpL0eR8V6SvQOr-19y4BBo
      //      ApiKey = "AIzaSyDrDkd4cYr21RpL0eR8V6SvQOr-19y4BBo;";
        }
        public Uri BaseUri { get; set; }
        
    }
}