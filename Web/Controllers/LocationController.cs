using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using JimJenkins.GeoCoding.Services;
using Web.Extensions;
using Web.Models;

namespace Web.Controllers
{
    public class ZipCodeController : ApiController
    {
        private readonly IGeoCodingService _geoCodingService;

        public ZipCodeController(IGeoCodingService geoCodingService)
        {
          _geoCodingService = geoCodingService;
        }

        
        // GET: api/Location/5
        [HttpGet]
        [Route("api/zipcode/{zipcode}")]
        public CoordinateViewModel Get(string zipCode)
        {
            var result = _geoCodingService.GetFromZip(zipCode);
            return new CoordinateViewModel
            {
                Latitude = result.Coordinate.Latitude,
                Longitude = result.Coordinate.Longitude,
                Description = result.GetDescription()
            };
        }

        
        
    }
}
