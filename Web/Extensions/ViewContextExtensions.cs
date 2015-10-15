using System.Web.Mvc;

namespace Web.Extensions
{
    public static class ViewContextExtensions
    {
        public static bool IsHomePage(this ViewContext viewContext)
        {
            return viewContext.RouteData.IsHomePageRoute();
        }
    }
}