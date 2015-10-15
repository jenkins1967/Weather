using System;
using System.Web.Routing;

namespace Web.Extensions
{
    public static class RouteDataExtensions
    {
        public static bool IsHomePageRoute(this RouteData routeData)
        {
            var rvDictionary = routeData.Values;
            try
            {
                return rvDictionary["controller"].ToString().Equals("Home", StringComparison.InvariantCultureIgnoreCase) && 
                    rvDictionary["action"].ToString().Equals("Index", StringComparison.InvariantCultureIgnoreCase);
            }
            catch
            {
                return false;
            }
        }
    }
}