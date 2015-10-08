using System;
using System.Collections;
using System.Linq;
using System.ServiceModel.Security;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace JimJenkins.GeoCoding.Services
{
    public class GeoCodingResult
    {
        public Coordinate Coordinate { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public string Country { get; set; }
    }
}
