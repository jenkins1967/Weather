using System.Collections.Generic;
using System.ServiceModel.Security.Tokens;
using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    internal class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }

        [JsonProperty("types")]
        public IList<string> Types { get; set; }
    }
}