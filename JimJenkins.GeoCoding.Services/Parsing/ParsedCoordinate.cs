using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    internal class ParsedCoordinate
    {
        [JsonProperty("lat")]
        public float Latitude { get; set; }

        [JsonProperty("lng")]
        public float Longitude { get; set; }
    }
}