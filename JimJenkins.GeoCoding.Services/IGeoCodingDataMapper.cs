using System;
using System.Collections.Generic;
using System.Linq;
using JimJenkins.GeoCoding.Services.Exceptions;
using JimJenkins.GeoCoding.Services.Parsing;

namespace JimJenkins.GeoCoding.Services
{
    public interface IGeoCodingDataMapper
    {
        IEnumerable<GeoCodingResult> MapRequestResult(RequestResult requestResult);
    }

    public class GeoCodingDataMapper : IGeoCodingDataMapper
    {
        public IEnumerable<GeoCodingResult> MapRequestResult(RequestResult requestResult)
        {
            return requestResult.Result.Select(MapRequestResult);
        }

        private GeoCodingResult MapRequestResult(Result resultObj)
        {
            try
            {
                var geoCodingResult = new GeoCodingResult
                {
                    Coordinate = new Coordinate(resultObj.Geometry.Location.Latitude, resultObj.Geometry.Location.Longitude)
                };

                var city = resultObj.AddressComponents.FirstOrDefault(x => x.IsCity);
                geoCodingResult.City = (city != null) ? city.LongName : string.Empty;

                var state = resultObj.AddressComponents.FirstOrDefault(x => x.IsState);
                geoCodingResult.State = (state != null) ? state.LongName : string.Empty;

                var postalCode = resultObj.AddressComponents.FirstOrDefault(x => x.IsPostalCode);
                geoCodingResult.Zip = (postalCode != null) ? postalCode.LongName : string.Empty;

                var country = resultObj.AddressComponents.FirstOrDefault(x => x.IsCountry);
                geoCodingResult.Country = (country != null) ? country.LongName : string.Empty;

                return geoCodingResult;
            }
            catch (Exception err)
            {
                throw new GeoCodingMapException(err);
            }
        }

    }
}