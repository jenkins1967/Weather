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
using Web.Services;
using WebGrease.Css.Extensions;

namespace Web.Controllers
{
    public class SearchController : ApiController
    {
        private readonly IGeoCodingService _geoCodingService;
        private readonly ICoordinateViewModelFactory _viewModelFactory;

        public SearchController(IGeoCodingService geoCodingService, ICoordinateViewModelFactory viewModelFactory)
        {
            _geoCodingService = geoCodingService;
            _viewModelFactory = viewModelFactory;
        }


        // GET: api/Location/5
        [HttpGet]
        //[Route("api/zipcode/{zipcode}", Name = "postalCodeSearch")]
        public CoordinateViewModel FindZip(string zipCode)
        {
            var result = _geoCodingService.GetFromZip(zipCode);
            return _viewModelFactory.Create(result);
        }

        [HttpGet]
     //   [Route("api/address/{address}", Name="addressSearch")]
        public IEnumerable<CoordinateViewModel> FindAddress(string address)
        {
            var result = _geoCodingService.GetFromAddress(address);
            return _viewModelFactory.Create(result).ToList();
        }

        [HttpGet]
        public CoordinateViewModel FindCoordinate([FromUri] CoordinateViewModel viewModel)
        {          
           var result = _geoCodingService.GetFromCoordinate(new Coordinate(viewModel.Latitude, viewModel.Longitude));
            //var result =
            //    new GeoCodingResult
            //    {
            //        City = "Trenton",
            //        Coordinate = new Coordinate(viewModel.Latitude, viewModel.Longitude),
            //        Country = "United States",
            //        State = "NJ"
            //    };
            
            if (result != null)
            {
                return _viewModelFactory.Create(result);
            }
            return null;
        }
    }
}
