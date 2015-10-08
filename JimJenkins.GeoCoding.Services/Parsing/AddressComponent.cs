using System.Collections.Generic;
using System.ServiceModel.Security.Tokens;
using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public IList<string> Types { get; set; }

        public bool IsCity { get { return Types.Contains("locality"); } }
        public bool IsState { get { return Types.Contains("administrative_area_level_1"); } }

        public bool IsCountry { get { return Types.Contains("country"); } }

        public bool IsPostalCode { get { return Types.Contains("postal_code"); } }
    }
}