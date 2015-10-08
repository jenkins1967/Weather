using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    public class ParsedCoordinate
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}