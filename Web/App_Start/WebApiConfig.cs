using System.Web.Http;

namespace Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name:"FindZip", 
                routeTemplate:"api/Search/Zip/{zipcode}",
                defaults: new {controller="Search", action="FindZip", zipcode=RouteParameter.Optional}
                );

            config.Routes.MapHttpRoute(
                name: "FindAddress",
                routeTemplate: "api/Search/Address/{address}",
                defaults: new { controller = "Search", action = "FindAddress", address=RouteParameter.Optional }
                );
            config.Routes.MapHttpRoute(
                name: "FindCoordinate",
                routeTemplate: "api/Search/Coordinate/",
                defaults: new { controller = "Search", action = "FindCoordinate"}
                );

            config.Routes.MapHttpRoute(
                name: "WeatherForLocation",
                routeTemplate: "api/Weather/",
                defaults: new { controller = "Weather", action = "WeatherForLocation" }
                );

            //WeatherForLocation
        }
    }
}
