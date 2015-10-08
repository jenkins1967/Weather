using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    public class Geometry
    {
        [JsonProperty("bounds")]
        public Bounds Bounds { get; set; }

        [JsonProperty("location")]
        public ParsedCoordinate Location { get; set; }
     
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
    }
}