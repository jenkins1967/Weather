using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Extensions
{
    public static class StringExtensions
    {
        public static IHtmlString AsHtmlString(this string value)
        {
            return new HtmlString(value);
        }
    }
}