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

        }
        public Uri BaseUri { get; set; }
        
    }
}