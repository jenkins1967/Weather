using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;

namespace JimJenkins.Weather.WeatherGov.DataService
{
    public interface IRequestLocationProvider
    {
        IList<KeyValuePair<string, string>> Build();
    }

    public class ZipCodeRequestLocationProvider : IRequestLocationProvider
    {
        public ZipCodeRequestLocationProvider(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                throw new ArgumentNullException();
            }
            ZipCode = zipCode;
        }

        public string ZipCode { get; private set; }

        public IList<KeyValuePair<string, string>> Build()
        {
            var list = new List<KeyValuePair<string, string>> {new KeyValuePair<string, string>("zipCodeList", ZipCode)};
            return list;
        }
    }

    public class CoordinateRequestLocationProvider : IRequestLocationProvider
    {
        public CoordinateRequestLocationProvider(float latitude, float longitude)
        {
            if (!LatLongIsValid(latitude))
            {
                throw new ArgumentOutOfRangeException("latitude");
            }

            if (!LatLongIsValid(longitude))
            {
                throw new ArgumentOutOfRangeException("longitude");
            }

            Latitude = latitude;
            Longitude = longitude;
        }

        private bool LatLongIsValid(float value)
        {
            return value >= -180 && value <= 180;
        }

        public float Latitude { get; private set; }
        public float Longitude { get; private set; }
        public IList<KeyValuePair<string, string>> Build()
        {
            var list = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("lat", Latitude.ToString(CultureInfo.InvariantCulture)),
                new KeyValuePair<string, string>("lon", Longitude.ToString(CultureInfo.InvariantCulture))
            };
            return list;
        }
    }
}