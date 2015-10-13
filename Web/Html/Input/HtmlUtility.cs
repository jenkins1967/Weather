using System.Collections.Generic;

namespace Web.Html.Input
{
    public static class HtmlUtility
    {
        public static void AddToClasses(string className, IDictionary<string, object> htmlAttributes)
        {
            var classes = (htmlAttributes.ContainsKey("class")) ? htmlAttributes["class"].ToString() : string.Empty;
            classes = (classes + " " + className).Trim();
            htmlAttributes["class"] = classes;
        }


    }
}