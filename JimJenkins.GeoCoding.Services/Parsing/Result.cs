using System.Collections.Generic;
using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    internal class Result {
        [JsonProperty("address_components")]
        public IList<AddressComponent> AddressComponents { get; set; }

        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }

        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }

        [JsonProperty("place_id")]
        public string PlaceId { get; set; }

        [JsonProperty("postcode_localities")]
        public IList<string> PostCodeLocalities { get; set; }
        
        [JsonProperty("types")]
        public IList<string> Types { get; set; }
    }
}