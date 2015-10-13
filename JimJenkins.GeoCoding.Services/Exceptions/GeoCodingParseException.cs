using System;

namespace JimJenkins.GeoCoding.Services.Exceptions
{
    public class GeoCodingParseException:Exception
    {
        public GeoCodingParseException(Exception innerException)
            : base("An exception occurred while parsing geocoding data.", innerException)
        {

        }
    }

    public class GeoCodingMapException : Exception
    {
        public GeoCodingMapException(Exception innerException)
            :base("An exception occurred while mapping geocoding data.", innerException)
        {

        }
    }
}
