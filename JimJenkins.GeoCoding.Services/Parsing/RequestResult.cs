﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace JimJenkins.GeoCoding.Services.Parsing
{
    public class RequestResult
    {
        [JsonProperty("results")]
        public IList<Result> Result { get; set; }
        
        [JsonProperty("status")]
        public string Status { get; set; }
    }
}