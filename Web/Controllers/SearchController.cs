using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Razor.Generator;
using JimJenkins.GeoCoding.Services;
using JimJenkins.Weather.WeatherGov.Entities.Parsing;
using Web.Extensions;
using Web.Models;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IGeoCodingService _geoCodingService;

        public SearchController(IGeoCodingService geoCodingService)
        {
          _geoCodingService = geoCodingService;
        }

        
        // GET: api/Location/5
        [HttpGet]
        //[Route("api/zipcode/{zipcode}", Name = "postalCodeSearch")]
        public CoordinateViewModel FindZip(string zipCode)
        {
            var result = _geoCodingService.GetFromZip(zipCode);
            return new CoordinateViewModel
            {
                Latitude = result.Coordinate.Latitude,
                Longitude = result.Coordinate.Longitude,
                Description = result.GetDescription()
            };
        }

        [HttpGet]
     //   [Route("api/address/{address}", Name="addressSearch")]
        public IEnumerable<CoordinateViewModel> FindAddress(string address)
        {
            var result = _geoCodingService.GetFromAddress(address);
            return result.Select(r =>
                new CoordinateViewModel
                {
                    Latitude = r.Coordinate.Latitude,
                    Longitude = r.Coordinate.Longitude,
                    Description = r.GetDescription()
                }).ToList();
        }

        [HttpGet]
        public CoordinateViewModel FindCoordinate([FromUri] CoordinateViewModel viewModel)
        {          
            var result = _geoCodingService.GetFromCoordinate(new Coordinate(viewModel.Latitude, viewModel.Longitude));
            if (result != null)
            {
                return new CoordinateViewModel
                {
                    Latitude = result.Coordinate.Latitude,
                    Longitude = result.Coordinate.Longitude,
                    Description = result.GetDescription()
                };
            }
            return null;
        }
    }
}
