using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    public class Bounds
    {
        [JsonProperty("northeast")]
        public ParsedCoordinate UpperLeft { get; set; }

        [JsonProperty("southwest")]
        public ParsedCoordinate LowerRight { get; set; }
    }
}