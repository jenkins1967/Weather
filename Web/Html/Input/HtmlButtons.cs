using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using Web.Extensions;

namespace Web.Html.Input
{
    public static class HtmlButtons
    {
        
        public static IHtmlString DefaultButton(this HtmlHelper html, string caption, IDictionary<string, object> htmlAttributes)
        {
            var builder = html.Button(caption, htmlAttributes);
            builder.AddCssClass("btn-default");
            return builder.ToString(TagRenderMode.Normal).AsHtmlString();
        }
        public static IHtmlString DefaultButton(this HtmlHelper html, string caption)
        {
            return html.DefaultButton(caption, null);
        }

        private static TagBuilder Button(this HtmlHelper html, string caption, IDictionary<string, object> htmlAttributes)
        {
            var attribs = htmlAttributes ?? new Dictionary<string, object>();
            var builder = new TagBuilder("button");
            builder.AddCssClass("btn");
            builder.MergeAttributes(attribs, false);
            return builder;
        }
    }
}