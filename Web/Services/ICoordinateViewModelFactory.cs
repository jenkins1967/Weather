using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using JimJenkins.GeoCoding.Services;
using Web.Extensions;
using Web.Models;

namespace Web.Services
{
    public interface ICoordinateViewModelFactory
    {
        CoordinateViewModel Create(GeoCodingResult result);
        IEnumerable<CoordinateViewModel> Create(IEnumerable<GeoCodingResult> result);
    }

    public class CoordinateViewModelFactory : ICoordinateViewModelFactory
    {
        public CoordinateViewModel Create(GeoCodingResult result)
        {
            return new CoordinateViewModel
            {
                Latitude = result.Coordinate.Latitude,
                Longitude = result.Coordinate.Longitude,
                Description = result.GetDescription()
            };
        }

        public IEnumerable<CoordinateViewModel> Create(IEnumerable<GeoCodingResult> result)
        {
            return result.Select(Create);
        }
    }
}